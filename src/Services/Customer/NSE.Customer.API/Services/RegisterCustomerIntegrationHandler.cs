using EasyNetQ;
using FluentValidation.Results;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Customer.API.Application.Commands;

namespace NSE.Customer.API.Services;

public class RegisterCustomerIntegrationHandler : BackgroundService
{
    private IBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus = RabbitHutch.CreateBus("host=localhost:5672");

        _bus.Rpc.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(
            async request => new ResponseMessage(
                    await RegisterCustomer(request)
                )
            );

        return Task.CompletedTask;
    }

    private async Task<ValidationResult> RegisterCustomer(UserRegisteredIntegrationEvent message)
    {
        var customerCommand = new RegisterCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);

        ValidationResult success;
        
        // Service locator
        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            success = await mediator.SendCommand(customerCommand);
        }

        return success;
    }
}