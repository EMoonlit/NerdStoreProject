using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.ShoppingCart.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.ShoppingCart.API.Controllers;

[Authorize]
public class ShoppingCartController : MainController
{

    private readonly IAspNetUser _user;

    public ShoppingCartController(IAspNetUser user)
    {
        _user = user;
    }

    [HttpGet("shopping-cart")]
    public async Task<ShoppingCartCustomer> GetShoppingCart()
    {
        _user.GetUserId();
        return null;
    }
    
    [HttpPost("shopping-cart")]
    public async Task<IActionResult> AddItemToShoppingCart(ShoppingCartItem item)
    {
        return null;
    }
    
    [HttpPut("shopping-cart/{productId}")]
    public async Task<IActionResult> UpdateItemInTheShoppingCart(Guid productId, ShoppingCartItem item)
    {
        return null;
    }
    
    [HttpDelete("shopping-cart/{productId}")]
    public async Task<IActionResult> RemoveItemFromTheShoppingCart(Guid productId)
    {
        return null;
    }
}