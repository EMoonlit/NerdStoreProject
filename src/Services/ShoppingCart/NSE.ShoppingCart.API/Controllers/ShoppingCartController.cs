using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSE.ShoppingCart.API.Data;
using NSE.ShoppingCart.API.Models;
using NSE.WebAPI.Core.Controllers;
using NSE.WebAPI.Core.User;

namespace NSE.ShoppingCart.API.Controllers;

[Authorize]
public class ShoppingCartController : MainController
{

    private readonly IAspNetUser _user;
    private readonly ShoppingCartContext _context;

    public ShoppingCartController(IAspNetUser user, ShoppingCartContext context)
    {
        _user = user;
        _context = context;
    }

    [HttpGet("shopping-cart")]
    public async Task<ShoppingCartCustomer> GetShoppingCart()
    {
        return await GetTheCustomerCart();
    }

    [HttpPost("shopping-cart")]
    public async Task<IActionResult> AddItemToShoppingCart(ShoppingCartItem item)
    {
        var shoppingCart = await GetTheCustomerCart();
        
        shoppingCart.AddItem(item);
        
        _context.ShoppingCartCustomer.Add(shoppingCart);
        
        if (!IsValidOperation()) return CustomResponse();

        var result = await _context.SaveChangesAsync();
        if (result <= 0) AddProcessError("The data could not be persisted in the database");

        return CustomResponse();
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

    private async Task<ShoppingCartCustomer> GetTheCustomerCart()
    {
        var shoppingCart = await _context.ShoppingCartCustomer
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());

        return shoppingCart ?? new ShoppingCartCustomer(_user.GetUserId());
    }
}