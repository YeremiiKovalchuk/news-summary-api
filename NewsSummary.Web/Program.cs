using NewsSummary.Core.Extensions;
using NewsSummary.Core.Models;
using NewsSummary.Infrastructure.Extensions;
using NewsSummary.Infrastructure.Models;
using NewsSummary.Web.Extensions;
using System.ComponentModel.DataAnnotations;
using NewsSummary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NewsSummary.Core.Interfaces;
using NewsSummary.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddWebDefaults();

builder.Services.AddEndpointsApiExplorer(); //web
builder.Services.AddSwaggerGen(); //web

builder.Services.AddCommonHttpClients();
builder.Services.AddCommonUseCases();

var sqlConnectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDb(sqlConnectionString!);

var redisSettings = builder.Configuration.GetSection("RedisRetryPolicy").Get<RedisRetryPolicy>();
var validationResult = new List<ValidationResult>();
if (!Validator.TryValidateObject(redisSettings, new ValidationContext(redisSettings), validationResult))
{
    foreach (var validation in validationResult)
    {
        Console.WriteLine(validation.ErrorMessage);
    }
    throw new ArgumentException("Invalid Redis parameters");
}

builder.Services.AddRedis(builder.Configuration.GetConnectionString("Redis")!, redisSettings);
builder.Services.AddCommonAutoMappers();

builder.Services.AddOptions<ApiKeys>().Bind(builder.Configuration.GetSection("ApiKeys"));

var app = builder.Build();

if (app.Environment.IsDevelopment()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 
app.UseAuthorization(); 
app.MapControllers(); 

app.Run();

public partial class Program { }

