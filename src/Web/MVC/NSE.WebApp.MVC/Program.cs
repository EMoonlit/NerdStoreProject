using NSE.WebApp.MVC.Config;

var builder = WebApplication.CreateBuilder(args);


// Auth configuration
builder.Services.AddAuthConfiguration();

// Add services to the container.
builder.Services.AddWebAppConfiguration(builder.Configuration);

// Add Injection Services
builder.Services.RegisterServices(builder.Environment);

var app = builder.Build();

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});
app.UseHttpsRedirection();
app.UseWebAppConfiguration(app.Environment);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();