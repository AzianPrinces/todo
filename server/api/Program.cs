using System.Text.Json;
using api;
using efscaffold.Entities;
using Infrastructure.Postgres.Scaffolding;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var appOptions = builder.Services.AddAppOptions(builder.Configuration);

Console.WriteLine("the app options are: " + JsonSerializer.Serialize(appOptions));

Console.WriteLine($"Connection string: {appOptions.DbConnectionString}");

builder.Services.AddDbContext<MyDbContext>(conf =>
{
    conf.UseNpgsql(appOptions.DbConnectionString);
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(config => config
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .SetIsOriginAllowed(x => true));

app.MapGet("/", (
    
    [FromServices]IOptionsMonitor<AppOptions> optionsMonitor,
    [FromServices] MyDbContext dbContext) =>
{
    
    
    
    var myTodo = new Todo()
    {
        Id = Guid.NewGuid().ToString(),
        Title = "My first todo",
        Description = "This is my first todo",
        Isdone = false,
        Priority = 5
    };
    dbContext.Todos.Add(myTodo);
    dbContext.SaveChanges();
    
    var objects = dbContext.Todos.ToList();
    return objects;
    
});

app.Run();
