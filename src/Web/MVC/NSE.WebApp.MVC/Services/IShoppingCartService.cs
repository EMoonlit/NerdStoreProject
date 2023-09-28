using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Services.Handlers;

public interface IShoppingCartService
{
    Task<ShoppingCartViewModel> GetShoppingCart();
    Task<ResponseResult> AddItemShoppingCart(ItemViewModel item);
    Task<ResponseResult> UpdateItemShoppingCart(Guid itemId, ItemViewModel item);
    Task<ResponseResult> RemoveItemShoppingCart(Guid itemId);
}