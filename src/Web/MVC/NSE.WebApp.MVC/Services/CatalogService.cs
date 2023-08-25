using Microsoft.Extensions.Options;
using NSE.Identity.API.Extensions;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services;

public class CatalogService : Validation, ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient, IOptions<AppSettings> appSettings)
    {
        httpClient.BaseAddress = new Uri(appSettings.Value.CatalogUrl);
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAll()
    {
        var response = await _httpClient.GetAsync($"/catalog/products/");

        ValidateErrorResponse(response);

        return await DeserializeResponseObjectAsync<IEnumerable<ProductViewModel>>(response);
    }

    public async Task<ProductViewModel> GetById(Guid id)
    {
        var response = await _httpClient.GetAsync($"/catalog/products/{id}");

        ValidateErrorResponse(response);

        return await DeserializeResponseObjectAsync<ProductViewModel>(response);
    }
}