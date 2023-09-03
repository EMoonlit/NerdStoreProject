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
}