namespace Cart.Api.Dto;

public class GetCartOutput
{
    public List<GetCartOutputCartItem> CartItems { get; set; } = null!;
}

public class GetCartOutputCartItem
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public int ProductPrice { get; set; }
    public int Quantity { get; set; }
}
