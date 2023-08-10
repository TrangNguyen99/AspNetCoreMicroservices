namespace MessageBus.Core.Contracts;

public class CartCheckoutEvent
{
    public string UserId { get; set; } = null!;
    public List<CartCheckoutEventCartItem> CartItems { get; set; } = null!;
}

public class CartCheckoutEventCartItem
{
    public string ProductId { get; set; } = null!;
}
