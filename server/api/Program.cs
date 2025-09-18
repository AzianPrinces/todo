using System.Text.Json;
using api;
using api.Etc;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine("the app options are: " + JsonSerializer.Serialize(appOptions));
Console.WriteLine($"Connection string: {appOptions.DbConnectionString}");

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddDbContext<MyDbContext>((sp, conf) =>
{
    var opts = sp.GetRequiredService<IOptions<AppOptions>>().Value;
    if (string.IsNullOrWhiteSpace(opts?.DbConnectionString))
        throw new Exception("Missing DB connection string. Ensure env var `AppOptions__DbConnectionString` is set and correctly quoted.");
    conf.UseNpgsql(opts.DbConnectionString);
});

builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(config => config
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .SetIsOriginAllowed(x => true));


app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.UseExceptionHandler();
await app.GenerateApiClientsFromOpenApi("/../../client/src/generated-ts-client.ts");

app.Run();
