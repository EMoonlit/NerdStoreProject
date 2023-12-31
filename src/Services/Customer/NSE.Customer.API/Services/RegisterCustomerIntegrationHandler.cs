using FluentValidation.Results;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;
using NSE.Customer.API.Application.Commands;
using NSE.MessageBus;

namespace NSE.Customer.API.Services;

public class RegisterCustomerIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }
    
    private void SetResponder()
    {
        _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(
            async request => 
                await RegisterCustomer(request)
        );

        _bus.AdvancedBus.Connected += OnConnect;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        
        return Task.CompletedTask;
    }

    private void OnConnect(object s, EventArgs e)
    {
        SetResponder();
    }

    private async Task<ResponseMessage> RegisterCustomer(UserRegisteredIntegrationEvent message)
    {
        var customerCommand = new RegisterCustomerCommand(message.Id, message.Name, message.Email, message.Cpf);

        ValidationResult success;
        
        // Service locator
        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            success = await mediator.SendCommand(customerCommand);
        }

        return new ResponseMessage(success);
    }
}