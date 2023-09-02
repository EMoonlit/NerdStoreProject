using Microsoft.Extensions.Configuration;

namespace NSE.Core.Utils;

public static class ConfigurationExtensions
{
    public static string GetMessageQueueConnection(this IConfiguration configuration, string name)
    {
        var connection = configuration?.GetSection("MessageQueueConnection")?[name];
        if (string.IsNullOrEmpty(connection)) throw new NotImplementedException();
        return connection;
    }
}