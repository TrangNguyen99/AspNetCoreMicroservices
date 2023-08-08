namespace Order.Application.Queries.GetOrders;

public class GetOrdersOutput
{
    public List<GetOrdersOutput_Order> Orders { get; set; } = null!;
}

public class GetOrdersOutput_Order
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
}
