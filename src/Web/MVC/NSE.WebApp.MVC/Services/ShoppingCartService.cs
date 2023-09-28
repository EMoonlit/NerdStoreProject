using Microsoft.Extensions.Options;
using NSE.Identity.API.Extensions;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Services;

public class ShoppingCartService : Validation, IShoppingCartService
{
    private readonly HttpClient _httpClient;

    public ShoppingCartService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(settings.Value.ShoppingCartUrl);
    }

    public async Task<ShoppingCartViewModel> GetShoppingCart()
    {
        var response = await _httpClient.GetAsync("/shopping-cart/");

        ValidateErrorResponse(response);

        return await DeserializeResponseObjectAsync<ShoppingCartViewModel>(response);
    }

    public async Task<ResponseResult> AddItemShoppingCart(ItemViewModel item)
    {
        var itemContent = GetContent(item);

        var response = await _httpClient.PostAsync("/shopping-cart/", itemContent);

        if (!ValidateErrorResponse(response)) return await DeserializeResponseObjectAsync<ResponseResult>(response);

        return ReturnOk();
    }
    
    public async Task<ResponseResult> UpdateItemShoppingCart(Guid itemId, ItemViewModel item)
    {
        var itemContent = GetContent(item);

        var response = await _httpClient.PutAsync($"/shopping-cart/{item.ProductId}", itemContent);

        if (!ValidateErrorResponse(response)) return await DeserializeResponseObjectAsync<ResponseResult>(response);

        return ReturnOk();
    }
    
    public async Task<ResponseResult> RemoveItemShoppingCart(Guid itemId)
    {
        var response = await _httpClient.DeleteAsync($"/shopping-cart/{itemId}");

        if (!ValidateErrorResponse(response)) return await DeserializeResponseObjectAsync<ResponseResult>(response);

        return ReturnOk();
    }
}