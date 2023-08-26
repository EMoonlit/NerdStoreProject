using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Customer.API.Models;

namespace NSE.Customer.API.Data.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly CustomerContext _context;

    public CustomerRepository(CustomerContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Dispose()
    {
        _context.Dispose();
    }

    public void Add(Models.Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public async Task<IEnumerable<Models.Customer>> GetAllAsync()
    {
        return await _context.Customers.AsNoTracking().ToListAsync();
    }

    public Task<Models.Customer> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Models.Customer> GetByCpfAsync(string cpf)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.Cpf.CpfDigits == cpf);
    }

    public void AddAddress(Address customerAddress)
    {
        _context.Address.Add(customerAddress);
    }

    public async Task<Address> GetAddressByIdAsync(Guid id)
    {
        return await _context.Address.FirstOrDefaultAsync(e => e.CustomerId == id);

    }
}