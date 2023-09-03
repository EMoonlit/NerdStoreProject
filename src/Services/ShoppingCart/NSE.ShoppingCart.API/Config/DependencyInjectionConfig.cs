using NSE.ShoppingCart.API.Data;

namespace NSE.ShoppingCart.API.Config;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ShoppingCartContext>();
    }
}