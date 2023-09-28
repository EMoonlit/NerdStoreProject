using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Extensions;

public class ShoppingCartViewComponent : ViewComponent
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartViewComponent(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _shoppingCartService.GetShoppingCart() ?? new ShoppingCartViewModel());
    }
}