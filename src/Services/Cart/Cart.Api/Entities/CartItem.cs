namespace Cart.Api.Entities;

public class CartItem
{
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
}
