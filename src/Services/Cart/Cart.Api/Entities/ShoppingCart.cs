namespace Cart.Api.Entities
{
    public class ShoppingCart
    {
        public string Username { get; set; } = null!;
        public List<CartItem> CartItems { get; set; } = new();
    }
}
