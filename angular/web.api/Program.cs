using web.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container  
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(); // Remove AddOpenApi() and ensure AddSwaggerGen() is used  
builder.Services.AddSingleton<ITodoService, TodoService>();

// Add CORS for Angular frontend  
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.MapControllers();


app.Run();

