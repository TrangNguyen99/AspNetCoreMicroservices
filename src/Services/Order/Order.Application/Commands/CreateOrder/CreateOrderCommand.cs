using MediatR;

namespace Order.Application.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<CreateOrderCommandOutput>
{
    public string UserId { get; set; } = null!;
}
