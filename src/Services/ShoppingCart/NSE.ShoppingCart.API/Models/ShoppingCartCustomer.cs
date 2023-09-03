namespace NSE.ShoppingCart.API.Models;

public class ShoppingCartCustomer
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalValue { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

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
        // Validate item is ok!
        // TODO: throw errors
        if (!item.IsValid()) return;
        
        // Associate
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
}