using Microsoft.EntityFrameworkCore;
using NSE.ShoppingCart.API.Data;
using NSE.WebAPI.Core.Identity;


namespace NSE.ShoppingCart.API.Config;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShoppingCartContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("Total", builder => 
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }

    public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        
        app.UseRouting();
        
        app.UseCors("Total");
        
        app.UseAuthConfiguration();
    }
}