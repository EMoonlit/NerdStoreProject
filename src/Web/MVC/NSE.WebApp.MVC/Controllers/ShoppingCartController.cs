using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Controllers;

[Authorize]
public class ShoppingCartController : MainController
{
    private readonly ICatalogService _catalogService;
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(ICatalogService catalogService, IShoppingCartService shoppingCartService)
    {
        _catalogService = catalogService;
        _shoppingCartService = shoppingCartService;
    }

    [Route("shopping-cart")]
    public async Task<IActionResult> Index()
    {
        return View(await _shoppingCartService.GetShoppingCart());
    }

    [HttpPost]
    [Route("shopping-cart/add-item")]
    public async Task<IActionResult> AddItemShoppingCart(ItemViewModel item)
    {
        var product = await _catalogService.GetById(item.ProductId);
        
        IsValidShoppingCartItem(product, item.Quantity);
        
        if (!IsValidOperation()) return View("Index", await _shoppingCartService.GetShoppingCart()); 
        
        item.Name = product.Name;
        item.Value = product.Price;
        item.Image = product.Image;

       
        
        var response = await _shoppingCartService.AddItemShoppingCart(item);

        if (IsResponseError(response)) return View("Index", await _shoppingCartService.GetShoppingCart());
            
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("shopping-cart/update-item")]
    public async Task<IActionResult> UpdateItemShoppingCart(Guid itemId, int quantity)
    {
        var product = await _catalogService.GetById(itemId);
        IsValidShoppingCartItem(product, quantity);
        if (!IsValidOperation()) return View("Index", await _shoppingCartService.GetShoppingCart()); 
        
        var editedItem = new ItemViewModel { ProductId = itemId, Quantity = quantity };
        var response = await _shoppingCartService.UpdateItemShoppingCart(itemId, editedItem);
        
        if (IsResponseError(response)) return View("Index", await _shoppingCartService.GetShoppingCart());
        
        return RedirectToAction("Index");
    }
    
    [HttpPost]
    [Route("shopping-cart/remove-item")]
    public async Task<IActionResult> RemoveItemShoppingCart(Guid itemId)
    {
        var product = await _catalogService.GetById(itemId);
        if (product == null)
        {
            AddValidationError("Product not exists");
            return View("Index", await _shoppingCartService.GetShoppingCart()); 
        }
        
        var response = await _shoppingCartService.RemoveItemShoppingCart(itemId);
        
        if (IsResponseError(response)) return View("Index", await _shoppingCartService.GetShoppingCart());
        
        return RedirectToAction("Index");
    }

    private void IsValidShoppingCartItem(ProductViewModel product, int quantity)
    {
        if (product == null) AddValidationError("Product not exists");
        if (quantity < 1) AddValidationError($"Choice the less on unit of quantity of {product.Name}");
        if (quantity > product.QuantityInStock) AddValidationError("Done products in stock");
    }
}