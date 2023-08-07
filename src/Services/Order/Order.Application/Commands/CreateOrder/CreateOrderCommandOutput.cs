namespace Order.Application.Commands.CreateOrder;

public class CreateOrderCommandOutput
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
}
