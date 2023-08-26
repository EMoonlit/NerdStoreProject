using NSE.Core.DomainObjects;

namespace NSE.Customer.API.Models;

public interface ICustomerRepository : IRepository<Customer>
{
    void Add(Customer customer);
    
    Task<IEnumerable<Customer>> GetAllAsync();

    Task<Customer> GetByIdAsync(Guid id);

    Task<Customer> GetByCpfAsync(string cpf);

    void AddAddress(Address customerAddress);
    
    Task<Address> GetAddressByIdAsync(Guid id);
}