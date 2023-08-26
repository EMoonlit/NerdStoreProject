using System.Globalization;
using Microsoft.AspNetCore.Localization;
using NSE.Identity.API.Extensions;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Config;

public static class WebAppConfig
{
    public static void AddWebAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        
        var appSettingsSection = configuration.GetSection("AppSettings");
        services.Configure<AppSettings>(appSettingsSection);
    }

    public static void UseWebAppConfiguration(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        // Configure the HTTP request pipeline.
        // if (environment.IsDevelopment())
        // {
        //     app.UseDeveloperExceptionPage();
        // }
        // else
        // {
        //     app.UseExceptionHandler("/error/500");
        //     app.UseStatusCodePagesWithRedirects("/error/{0}");
        //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //     app.UseHsts();
        // }
        
        app.UseExceptionHandler("/error/500");
        app.UseStatusCodePagesWithRedirects("/error/{0}");
        app.UseHsts();
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthConfiguration();

        var supportedCultures = new[] { new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });

        app.UseMiddleware<ExceptionMiddleware>();
    }
}