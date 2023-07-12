namespace NSE.WebApp.MVC.Config;

public static class WebAppConfig
{
    public static void AddWebAppConfiguration(this IServiceCollection services)
    {
        services.AddControllersWithViews();
    }

    public static void UseWebAppConfiguration(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        // Configure the HTTP request pipeline.
        if (!environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthConfiguration();
    }
}