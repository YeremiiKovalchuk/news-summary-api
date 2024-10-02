using NewsSummary.Core.Extensions;
using NewsSummary.Core.Constants;
using NewsSummary.Core.Models;
using NewsSummary.Infrastructure.Extensions;
using NewsSummary.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using NewsSummary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using NewsSummary.Core.Interfaces;
using NewsSummary.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers() //web
.AddJsonOptions(options =>
{
    WebConstants.ApplyCommonSerializerOptions(options.JsonSerializerOptions);
});

builder.Services.AddDbContext<SummaryDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddTransient<ICityRepository, CityRepository>();

builder.Services.AddEndpointsApiExplorer(); //web
builder.Services.AddSwaggerGen(); //web

builder.Services.AddCommonHttpClients();
builder.Services.AddCommonUseCases();

var redisSettings = builder.Configuration.GetSection("RedisRetryPolicy").Get<RedisRetryPolicy>();
var validationResult = new List<ValidationResult>();
if (!Validator.TryValidateObject(redisSettings, new ValidationContext(redisSettings), validationResult))
{
    foreach (var validation in validationResult)
    {
        Console.WriteLine(validation.ErrorMessage);
    }
    return;
}

builder.Services.AddRedis(builder.Configuration.GetConnectionString("Redis")!, redisSettings);
builder.Services.AddCommonAutoMappers();

builder.Services.AddOptions<ApiKeys>().Bind(builder.Configuration.GetSection("ApiKeys"));

var app = builder.Build();
if (app.Environment.IsDevelopment()) //web
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection(); //web
app.UseAuthorization(); //web
app.MapControllers(); //web

app.Run();
