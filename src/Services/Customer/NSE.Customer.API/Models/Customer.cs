using NSE.Core.DomainObjects;

namespace NSE.Customer.API.Models;

public class Customer : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Cpf Cpf { get; private set; }
    public bool IsActive { get; private set; }
    public Address Address { get; private set; }

    // EF Relation
    protected Customer()
    {
    }

    public Customer(Guid id, string name, string email, string cpf)
    {
        Id = id;
        Name = name;
        Email = new Email(email);
        Cpf = new Cpf(cpf);
        IsActive = true;
    }

    public void EmailChenge(string email)
    {
        Email = new Email(email);
    }

    public void AddressChange(Address address)
    {
        Address = address;
    }
}