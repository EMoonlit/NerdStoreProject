using NSE.WebApp.MVC.Config;

var builder = WebApplication.CreateBuilder(args);


// Auth configuration
builder.Services.AddAuthConfiguration();

// Add services to the container.
builder.Services.AddWebAppConfiguration();

var app = builder.Build();

app.UseWebAppConfiguration(app.Environment);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();