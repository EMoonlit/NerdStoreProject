using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public interface ICatalogService
{
    Task<IEnumerable<ProductViewModel>> GetAll();

    Task<ProductViewModel> GetById(Guid id);
}