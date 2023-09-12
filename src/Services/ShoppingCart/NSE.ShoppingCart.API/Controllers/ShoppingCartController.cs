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
        return await GetCustomerCart() ?? new ShoppingCartCustomer();
    }

    [HttpPost("shopping-cart")]
    public async Task<IActionResult> AddItemToShoppingCart(ShoppingCartItem item)
    {
        var shoppingCart = await GetCustomerCart();
        
        if (shoppingCart == null)
            HandleNewShoppingCart(item);
        else
            HandleShoppingCart(shoppingCart, item);

        ValidateShoppingCart(shoppingCart);
        if (!IsValidOperation()) return CustomResponse();

        await PersistData();
        
        return CustomResponse();
    }

    [HttpPut("shopping-cart/{productId}")]
    public async Task<IActionResult> UpdateItemInTheShoppingCart(Guid productId, ShoppingCartItem item)
    {
        var shoppingCart = await GetShoppingCart();
        var itemShoppingCart = await GetItemValidatedShoppingCart(productId, shoppingCart, item);
        if (itemShoppingCart == null) return CustomResponse();
        
        shoppingCart.UpdateUnits(itemShoppingCart, item.Quantity);
        
        ValidateShoppingCart(shoppingCart);
        if (!IsValidOperation()) return CustomResponse();
        
        _context.ShoppingCartItems.Update(itemShoppingCart);
        _context.ShoppingCartCustomer.Update(shoppingCart);

        await PersistData();
        
        return CustomResponse();
    }

    [HttpDelete("shopping-cart/{productId}")]
    public async Task<IActionResult> RemoveItemFromTheShoppingCart(Guid productId)
    {
        var shoppingCart = await GetShoppingCart();
        var itemShoppingCart = await GetItemValidatedShoppingCart(productId, shoppingCart);
        if (itemShoppingCart == null) return CustomResponse();

        
        ValidateShoppingCart(shoppingCart);
        if (!IsValidOperation()) return CustomResponse();
        
        shoppingCart.RemoveItem(itemShoppingCart);

        _context.ShoppingCartItems.Remove(itemShoppingCart);
        _context.ShoppingCartCustomer.Update(shoppingCart);

        await PersistData();
        
        return CustomResponse();
    }

    private async Task<ShoppingCartCustomer?> GetCustomerCart()
    {
        return await _context.ShoppingCartCustomer
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CustomerId == _user.GetUserId());
    }
    
    private void HandleNewShoppingCart(ShoppingCartItem item)
    {
        var shoppingCart = new ShoppingCartCustomer(_user.GetUserId());
        shoppingCart.AddItem(item);

        ValidateShoppingCart(shoppingCart);
        
        _context.ShoppingCartCustomer.Add(shoppingCart);
    }
    
    private void HandleShoppingCart(ShoppingCartCustomer shoppingCart, ShoppingCartItem item)
    {
        var isProductInTheCart = shoppingCart.DoesTheItemAlreadyExistInTheCart(item);

        shoppingCart.AddItem(item);
        
        ValidateShoppingCart(shoppingCart);

        if (isProductInTheCart)
        {
            _context.ShoppingCartItems.Update(shoppingCart.GetByProductId(item.ProductId));
        }
        else
        {
            _context.ShoppingCartItems.Add(item);
        }

        _context.ShoppingCartCustomer.Update(shoppingCart);
    }

    private async Task<ShoppingCartItem?> GetItemValidatedShoppingCart(Guid productId, ShoppingCartCustomer shoppingCart,
        ShoppingCartItem item = null)
    {
        if (item != null && productId != item.ProductId)
        {
            AddProcessError("The item does not match the information provided");
            return null;
        }

        if (shoppingCart == null)
        {
            AddProcessError("Cart not found");
            return null;
        }

        var itemShoppingCart = await _context.ShoppingCartItems
            .FirstOrDefaultAsync(i => i.ShoppingCartId == shoppingCart.Id && i.ProductId == productId);

        if (itemShoppingCart == null || !shoppingCart.DoesTheItemAlreadyExistInTheCart(itemShoppingCart))
        {
            AddProcessError("The item is not in the cart");
            return null;
        }

        return itemShoppingCart;
    }

    private async Task PersistData()
    {
        var result = await _context.SaveChangesAsync();
        if (result <= 0) AddProcessError("The data could not be persisted in the database");
    }
    
    private bool ValidateShoppingCart(ShoppingCartCustomer shoppingCartCustomer)
    {
        if (shoppingCartCustomer.IsValid()) return true;

        shoppingCartCustomer.ValidationResult.Errors.ToList().ForEach(e => AddProcessError(e.ErrorMessage));
        return false;
    }
}