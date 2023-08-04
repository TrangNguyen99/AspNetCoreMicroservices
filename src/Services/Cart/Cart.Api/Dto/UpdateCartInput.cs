namespace Cart.Api.Dto;

public class UpdateCartInput
{
    public List<UpdateCartInput_CartItem> CartItems { get; set; } = null!;
}

public class UpdateCartInput_CartItem
{
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
}
