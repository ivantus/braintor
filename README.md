# tmp

Brief run guide for the Angular frontend and ASP.NET Core Web API.

## Run the API

From `C:\tmp\angular\web.api`:

```powershell
dotnet restore
dotnet run
```

The API starts at:

```text
http://localhost:5182
```

Useful endpoints:

```text
http://localhost:5182/swagger
http://localhost:5182/api/quiz
http://localhost:5182/api/todos
```

## Run the Frontend

Open a second terminal and run from `C:\tmp\angular\frontend`:

```powershell
npm install
npm start
```

The frontend starts at:

```text
http://localhost:4200
```

## Run Both

Use two terminals:

1. API: `cd C:\tmp\angular\web.api` then `dotnet run`
2. Frontend: `cd C:\tmp\angular\frontend` then `npm start`

Keep both processes running while developing.
