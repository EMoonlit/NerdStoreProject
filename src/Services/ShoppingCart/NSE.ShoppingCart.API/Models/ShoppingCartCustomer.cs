using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace NSE.ShoppingCart.API.Models;

public class ShoppingCartCustomer
{
    internal const int MAX_ITEM_QUANTITY = 5;
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalValue { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    
    public ValidationResult ValidationResult { get; set; }

    public ShoppingCartCustomer(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
    }
    
    public ShoppingCartCustomer() { }

    internal void CalculateTheTotalValueOfTheShoppingCart()
    {
        TotalValue = Items.Sum(p => p.CalculateTheTotalValueOfAnItemInTheCart());
    }

    internal bool DoesTheItemAlreadyExistInTheCart(ShoppingCartItem item)
    {
        return Items.Any(p => p.ProductId == item.ProductId);
    }

    internal ShoppingCartItem GetByProductId(Guid productId)
    {
        return Items.FirstOrDefault(p => p.ProductId == productId)!;
    }
    internal void AddItem(ShoppingCartItem item)
    {
        item.AssociateWithTheShoppingCart(Id);

        if (DoesTheItemAlreadyExistInTheCart(item))
        {
            var existingItem = GetByProductId(item.ProductId);
            existingItem.AddUnits(item.Quantity);

            item = existingItem;
            Items.Remove(existingItem);
        }
        
        Items.Add(item);

        CalculateTheTotalValueOfTheShoppingCart();
    }

    internal void UpdateItem(ShoppingCartItem item)
    {
        item.AssociateWithTheShoppingCart(Id);

        var existingItem = GetByProductId(item.ProductId);

        Items.Remove(existingItem);
        Items.Add(item);
        
        CalculateTheTotalValueOfTheShoppingCart();
    }

    internal void UpdateUnits(ShoppingCartItem item, int units)
    {
        item.UpdateUnits(units);
        UpdateItem(item);
    }

    internal void RemoveItem(ShoppingCartItem item)
    {
        var existingItem = GetByProductId(item.ProductId);
        Items.Remove(existingItem);
        CalculateTheTotalValueOfTheShoppingCart();
    }
    
    internal bool IsValid()
    {
        var errors = Items.SelectMany(i => new ShoppingCartItemValidation().Validate(i).Errors).ToList();
        errors.AddRange(new ShoppingCartCustomerValidation().Validate(this).Errors);
        ValidationResult = new ValidationResult(errors);

        return ValidationResult.IsValid;
    }
}

public class ShoppingCartCustomerValidation : AbstractValidator<ShoppingCartCustomer>
{
    public ShoppingCartCustomerValidation()
    {
        RuleFor(c => c.CustomerId)
            .NotEqual(Guid.Empty)
            .WithMessage("Unrecognized customer");

        RuleFor(c => c.Items.Count)
            .GreaterThan(0)
            .WithMessage("The cart has no items");

        RuleFor(c => c.TotalValue)
            .GreaterThan(0)
            .WithMessage("The total value of the cart must be greater than 0");
    }
}