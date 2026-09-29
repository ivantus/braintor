# ADR-0001: Near-zero-cost Azure hosting architecture

- Status: Accepted
- Date: 2026-09-29

## Context

Braintor needs hosting for:

- a web UI;
- a .NET API;
- persistent test, user, and test-run data;
- user authentication/registration;
- infrastructure that costs approximately $0/month at low traffic;
- infrastructure-as-code suitable for GitHub Actions.

The expected initial workload is hundreds of users, low API traffic, mostly text data, and a small frontend.

## Decision

Use an Azure-native serverless architecture:

```text
Internet
   |
   v
Azure Static Web Apps
   |
   | HTTPS / API calls
   v
Azure Functions (.NET isolated)
   |
   | Managed Identity
   v
Azure Cosmos DB for NoSQL

Authentication:
Azure Static Web Apps authentication / Azure identity
```

### Web UI

Use Azure Static Web Apps on the Free tier where possible.

Requirements:

- SPA hosting and routing;
- HTTPS;
- custom domain can be added later;
- built-in authentication where appropriate;
- initially support Microsoft identity;
- allow another OAuth/OIDC provider to be introduced later without redesigning the application.

### API

Use Azure Functions with the .NET isolated worker model.

Requirements:

- cheapest suitable consumption/serverless hosting;
- prefer Flex Consumption when compatible;
- scale to zero;
- no Premium or Dedicated App Service plan;
- HTTPS only;
- provision only required supporting resources.

### Database

Use Azure Cosmos DB for NoSQL with the lifetime Free Tier.

Requirements:

- `enableFreeTier: true`;
- do not use Cosmos DB Serverless if it prevents use of the lifetime Free Tier;
- use provisioned throughput compatible with the free 1,000 RU/s / 25 GB allowance;
- prefer shared database throughput where appropriate;
- initially create containers for `users`, `tests`, and `testRuns`.

Partition keys should be chosen for these access patterns:

- get test by ID;
- list tests/category;
- get user's test history;
- create test run;
- update/complete test run;
- get user's profile.

The schema should remain simple and document-oriented.

### Authentication and authorization

Use Azure Static Web Apps authentication where appropriate.

The API must derive the authenticated user's identity from trusted authentication claims and must not trust a user ID supplied by the browser.

Prepare for these logical roles:

- `authenticated-user`;
- `admin`.

Normal users will read tests, start tests, submit answers, and read their own results. Admins will eventually create, edit, and delete tests.

### Managed identity and secrets

Azure Functions must use a system-assigned managed identity to access Cosmos DB.

Application code should be able to use `DefaultAzureCredential`.

Assign minimum required Cosmos DB data-plane RBAC permissions. Do not store Cosmos account keys or connection strings in GitHub Secrets, Function settings, Bicep parameters, or repository files.

Expose the Cosmos endpoint as a non-secret application setting such as `Cosmos__Endpoint`.

### Infrastructure as Code

Use Bicep rather than Terraform because the selected infrastructure is Azure-only and Bicep avoids external state management.

Use modular Bicep, approximately:

```text
infra/
├── main.bicep
├── modules/
│   ├── static-web-app.bicep
│   ├── function-app.bicep
│   ├── storage.bicep
│   ├── cosmos.bicep
│   └── roles.bicep
├── parameters/
│   ├── dev.bicepparam
│   └── prod.bicepparam
└── README.md
```

At minimum parameterize `applicationName`, `environment`, and `location`. Generate deterministic names and apply common tags including `application`, `environment`, and `managed-by = bicep`.

Useful outputs should include:

- `staticWebAppHostname`;
- `functionHostname`;
- `cosmosEndpoint`;
- `cosmosDatabaseName`.

Never output secrets.

### GitHub Actions

Infrastructure must be ready for deployment from GitHub Actions using Azure workload identity federation/OIDC rather than a long-lived client secret.

The resource group is assumed to exist and deployment should work with:

```bash
az deployment group create \
  --resource-group <resource-group> \
  --template-file infra/main.bicep \
  --parameters infra/parameters/dev.bicepparam
```

### Cost constraints

Cost is a first-class architectural requirement.

Do not introduce API Management, AKS, Container Apps, Redis, Service Bus, Application Gateway, VNet, private endpoints, or Key Vault unless technically required.

Document expected cost behavior for Static Web Apps, Functions, Function Storage, Cosmos DB, network/data transfer, and authentication, including settings that could accidentally move the application outside the free/near-free range.

## Consequences

### Positive

- Very low fixed infrastructure cost.
- Serverless compute scales to zero.
- Cosmos DB has a substantial lifetime free allowance.
- Managed identity removes database credentials from application configuration.
- Azure-native Bicep keeps IaC simple.
- GitHub OIDC avoids long-lived deployment credentials.

### Trade-offs

- Cosmos DB is document-oriented rather than relational.
- The application is intentionally Azure-specific.
- Static Web Apps authentication may need to evolve if authentication requirements become more complex.
- Some supporting storage/network usage may create small charges even while primary services remain within free allowances.

## Implementation requirements

Before infrastructure work is considered complete:

1. Run Bicep linting.
2. Run Bicep build/compile validation and fix all errors.
3. Use current stable Azure resource API versions where practical.
4. Verify resource dependencies and idempotent RBAC assignments.
5. Verify Cosmos Free Tier configuration.
6. Verify the Function hosting SKU supports scale-to-zero behavior.
7. Ensure repeated deployments do not unnecessarily recreate resources.
8. If Azure CLI is authenticated, run a `what-if` deployment, but do not deploy resources unless explicitly instructed.


## Deployment flow

GitHub Actions is the deployment client for Azure. Bicep describes the desired infrastructure; a GitHub-hosted runner authenticates to Azure and submits that desired state to Azure Resource Manager (ARM).

```text
Developer -> git push -> GitHub Actions runner
                           |
                           +-- OIDC -> Microsoft Entra ID
                           |
                           +-- Bicep -> ARM
                           |            +-- Static Web App
                           |            +-- Function App
                           |            +-- Storage
                           |            +-- Cosmos DB
                           |            +-- Managed Identity / RBAC
                           |
                           +-- build/deploy .NET API -> Function App
                           +-- build/deploy frontend -> Static Web App
```

### GitHub to Azure authentication

Use GitHub Actions OIDC / Azure workload identity federation, not a long-lived Azure client secret.

Azure is configured once to trust GitHub tokens for the expected repository and deployment context (for example `ivantus/braintor` and the protected production branch/environment). The workflow uses non-secret identifiers such as `AZURE_CLIENT_ID`, `AZURE_TENANT_ID`, and `AZURE_SUBSCRIPTION_ID`.

```text
GitHub runner
    |
    | request signed short-lived OIDC token
    v
GitHub OIDC issuer
    |
    v
Microsoft Entra ID
    |
    | validate repository/ref/environment claims
    v
short-lived Azure access token
    |
    v
Azure Resource Manager
```

The workflow must request `id-token: write` and can authenticate with `azure/login`:

```yaml
permissions:
  id-token: write
  contents: read

steps:
  - uses: actions/checkout@v4

  - uses: azure/login@v2
    with:
      client-id: ${{ vars.AZURE_CLIENT_ID }}
      tenant-id: ${{ vars.AZURE_TENANT_ID }}
      subscription-id: ${{ vars.AZURE_SUBSCRIPTION_ID }}

  - name: Deploy infrastructure
    run: |
      az deployment group create \
        --resource-group braintor \
        --template-file infra/main.bicep \
        --parameters infra/parameters/prod.bicepparam
```

Scope the deployment identity to the minimum permissions required, preferably on the Braintor resource group rather than the whole subscription.

### Bicep and application deployment

The runner does not directly create Azure resources. It sends the Bicep deployment to ARM. ARM reconciles desired and actual state: missing resources are created, unchanged resources remain unchanged, and changed resources are updated where supported. Bicep must therefore be deterministic and idempotent.

Bicep provisions/configures hosting resources; application code is deployed separately. After infrastructure deployment, GitHub Actions should:

1. restore, test, and publish the .NET Function application;
2. deploy the published artifact to the Function App;
3. install dependencies, test, and build the frontend;
4. deploy the frontend artifact to Azure Static Web Apps.

This may initially be one workflow and later be split if useful.

### Runtime identity: Function to Cosmos DB

Deployment identity and runtime identity are separate:

```text
Deployment:
GitHub Actions -> OIDC -> Entra ID -> Azure Resource Manager

Runtime:
Azure Function -> system-assigned Managed Identity
               -> Entra ID
               -> Cosmos DB data-plane RBAC
               -> Cosmos DB
```

Bicep enables the Function's system-assigned managed identity and grants the minimum required Cosmos DB data-plane role. The application receives only the non-secret Cosmos endpoint and uses `DefaultAzureCredential`:

```csharp
var credential = new DefaultAzureCredential();

var client = new CosmosClient(
    configuration["Cosmos__Endpoint"],
    credential);
```

No Cosmos account key or database password is required.

### One-time bootstrap

There is a one-time bootstrap because GitHub cannot deploy Azure resources until Azure trusts the GitHub deployment identity.

Create/configure once:

```text
Azure subscription
       |
       +-- Resource Group: braintor
       |
       +-- Entra application / deployment identity
               |
               +-- federated GitHub credential
               |    repository: ivantus/braintor
               |    branch/environment: approved deployment context
               |
               +-- required RBAC on Braintor resource group
```

This may be performed manually or by an explicitly run bootstrap script and must not require a permanent Azure client secret. After bootstrap, normal infrastructure and application changes flow through GitHub Actions.

The target developer experience is:

```text
git push / merge
      |
      v
GitHub Actions
      |
      +-- OIDC authenticate to Azure
      +-- deploy Bicep
      +-- deploy API
      +-- deploy Web UI
      |
      v
Azure
```

A follow-up implementation should add `.github/workflows/deploy.yml` and document or script the one-time Azure/GitHub OIDC bootstrap.
