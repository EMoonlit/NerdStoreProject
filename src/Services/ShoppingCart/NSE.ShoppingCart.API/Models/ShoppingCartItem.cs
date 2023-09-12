using FluentValidation;

namespace NSE.ShoppingCart.API.Models;

public class ShoppingCartItem
{
    public ShoppingCartItem(Guid id)
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    public string Image { get; set; }
    public Guid ShoppingCartId { get; set; }
    
    public ShoppingCartCustomer ShoppingCartCustomer { get; set; }

    internal void AssociateWithTheShoppingCart(Guid shoppingCartId)
    {
        ShoppingCartId = shoppingCartId;
    }

    internal decimal CalculateTheTotalValueOfAnItemInTheCart()
    {
        return Quantity * Value;
    }

    internal void AddUnits(int units)
    {
        Quantity = units;
    }
    
    internal void UpdateUnits(int units)
    {
        Quantity += units;
    }

    internal bool IsValid()
    {
        return new ShoppingCartItemValidation().Validate(this).IsValid;
    }
}

public class ShoppingCartItemValidation : AbstractValidator<ShoppingCartItem>
{
    public ShoppingCartItemValidation()
    {
        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid product Id");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Invalid name of product");

        RuleFor(c => c.Quantity)
            .GreaterThan(0)
            .WithMessage(item => $"The minimum quantity for an item {item.Name} is 1");

        RuleFor(c => c.Quantity)
            .LessThan(100)
            .WithMessage(item => $"he maximum quantity for an item {item.Name} is 100");

        RuleFor(c => c.Value)
            .GreaterThan(0)
            .WithMessage(item => $"The value of an item {item.Name} must be greater than 0");
    }
}