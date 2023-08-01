using NSE.Core.DomainObjects;

namespace NSE.Catalog.API.Models;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product> GetByIdAsync(Guid id);

    void Add(Product product);

    void Update(Product product);
}