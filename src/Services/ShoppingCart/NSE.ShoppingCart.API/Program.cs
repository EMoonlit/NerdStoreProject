using System.Reflection;
using NSE.ShoppingCart.API.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.RegisterServices();

builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseApiConfiguration(builder.Environment);

app.MapControllers();

app.Run();