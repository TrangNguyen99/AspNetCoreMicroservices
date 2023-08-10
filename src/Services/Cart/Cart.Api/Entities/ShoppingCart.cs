namespace Cart.Api.Entities;

public class ShoppingCart
{
    public string UserId { get; set; } = null!;
    public List<CartItem> CartItems { get; set; } = null!;
}
