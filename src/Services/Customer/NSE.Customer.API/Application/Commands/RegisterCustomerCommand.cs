using FluentValidation;
using NSE.Core.Messages;

namespace NSE.Customer.API.Application.Commands;

public class RegisterCustomerCommand : Command
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }

    public RegisterCustomerCommand(Guid id, string name, string email, string cpf)
    {
        AggregateId = id;
        Id = id;
        Name = name;
        Email = email;
        Cpf = cpf;
    }

    public override bool IsValid()
    {
        ValidationResult = new RegisterCustomerValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("customer id is invalid");

        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("customer name is not valid");

        RuleFor(c => c.Cpf)
            .Must(HasValidCpf)
            .WithMessage("customer document not valid");

        RuleFor(c => c.Email)
            .Must(HasValidEmail)
            .WithMessage("customer email is not valid");
    }
    
    protected static bool HasValidCpf(string cpf)
    {
        return Core.DomainObjects.Cpf.IsValid(cpf);
    }

    protected static bool HasValidEmail(string email)
    {
        return Core.DomainObjects.Email.IsValid(email);
    }
}


