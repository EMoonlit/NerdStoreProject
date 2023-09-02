using FluentValidation.Results;
using MediatR;
using NSE.Core.Mediator;
using NSE.Customer.API.Application.Commands;
using NSE.Customer.API.Application.Events;
using NSE.Customer.API.Data;
using NSE.Customer.API.Data.Repository;
using NSE.Customer.API.Models;
using NSE.Customer.API.Services;

namespace NSE.Customer.API.Config;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();

        services.AddScoped<INotificationHandler<RegisteredCustomerEvent>, CustomerEventHandler>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerContext>();
        
    }
}