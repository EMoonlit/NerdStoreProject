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
}