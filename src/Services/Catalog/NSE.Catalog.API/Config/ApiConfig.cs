using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Data;
using NSE.WebAPI.Core.Identity;

namespace NSE.Identity.API.Config;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
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

        return services;
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