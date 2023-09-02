using NSE.Core.Utils;
using NSE.Customer.API.Services;
using NSE.MessageBus;

namespace NSE.Customer.API.Config;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<RegisterCustomerIntegrationHandler>();
    }
}