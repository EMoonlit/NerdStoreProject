using MediatR;

namespace NSE.Customer.API.Application.Events;

public class CustomerEventHandler : INotificationHandler<RegisteredCustomerEvent>
{
    public Task Handle(RegisteredCustomerEvent notification, CancellationToken cancellationToken)
    {
        // Send confirmation event
        return Task.CompletedTask;
    }
}