using MediatR;

namespace Order.Application.Queries.GetOrders;

public class GetOrdersQuery : IRequest<GetOrdersOutput>
{
    public string UserId { get; set; } = null!;
}
