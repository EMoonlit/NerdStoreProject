using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Config;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IWebHostEnvironment environment)
    {
        services.AddHttpClient<IAuthService, AuthService>()
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();

                if (environment.IsDevelopment())
                {
                    handler.ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                }

                return handler;
            });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<IUser, AspNetUser>();
    }
}