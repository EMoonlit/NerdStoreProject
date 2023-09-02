using NSE.Core.Utils;
using NSE.MessageBus;

namespace NSE.Identity.API.Config;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
    }
}