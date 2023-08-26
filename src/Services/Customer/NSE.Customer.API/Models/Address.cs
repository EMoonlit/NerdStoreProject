using NSE.Core.DomainObjects;

namespace NSE.Customer.API.Models;

public class Address : Entity
{
    public string StreetAddress { get; private set; }
    public string StreetAddressLine2 { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    public Guid CustomerId { get; private set; }

    public Customer Customer { get; private set; }
    
    public Address(string streetAddress, string streetAddressLine2, string city, string state, string zipCode)
    {
        StreetAddress = streetAddress;
        StreetAddressLine2 = streetAddressLine2;
        City = city;
        State = state;
        ZipCode = zipCode;
    }
}