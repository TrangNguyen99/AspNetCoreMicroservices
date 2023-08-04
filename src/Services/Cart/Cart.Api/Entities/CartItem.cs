namespace Cart.Api.Entities;

public class CartItem
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public int ProductPrice { get; set; }
    public int Quantity { get; set; }
}
