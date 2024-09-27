using NewsSummary.Core.Interfaces;
using NewsSummary.Core.Services.UseCases;
using NewsSummary.Core.Extensions;
using NewsSummary.Core.Constants;
using NewsSummary.Core.Models;
using NewsSummary.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers() //web
.AddJsonOptions(options =>
{
    WebConstants.ApplyCommonSerializerOptions(options.JsonSerializerOptions);
});

builder.Services.AddEndpointsApiExplorer(); //web
builder.Services.AddSwaggerGen(); //web

builder.Services.AddCommonHttpClients();
builder.Services.AddCommonUseCases();

builder.Services.AddCommonCaching(builder.Configuration);
builder.Services.AddCommonAutoMappers();

builder.Configuration.AddUserSecrets("c0d704df-fc2d-4a0b-b3a5-e17706fcf769");
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
