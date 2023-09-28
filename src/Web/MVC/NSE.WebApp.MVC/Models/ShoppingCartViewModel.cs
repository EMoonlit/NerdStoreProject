namespace NSE.WebApp.MVC.Models;

public class ShoppingCartViewModel
{
    public decimal TotalValue { get; set; }
    public List<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
}

public class ItemViewModel
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = "";
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    public string Image { get; set; } = "";
}