using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using NSE.Customer.API.Application.Events;
using NSE.Customer.API.Models;

namespace NSE.Customer.API.Application.Commands;

public class CustomerCommandHandler : CommandHandler, IRequestHandler<RegisterCustomerCommand, ValidationResult>
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }
    
    public async Task<ValidationResult> Handle(RegisterCustomerCommand message, CancellationToken cancellationToken)
    {
        if (!message.IsValid()) return message.ValidationResult;

        var customer = new Models.Customer(message.Id, message.Name, message.Email, message.Cpf);

        var validCustomer = await _customerRepository.GetByCpfAsync(customer.Cpf.CpfDigits);

        if (validCustomer != null)
        {
            AddError("This document already in use");
            return ValidationResult;
        }
        
        _customerRepository.Add(customer);
        
        customer.AddEvent(new RegisteredCustomerEvent(message.Id, message.Name, message.Email, message.Cpf));
        
        return await PersistData(_customerRepository.UnitOfWork);
    }
}