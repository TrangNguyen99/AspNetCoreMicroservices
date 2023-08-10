using MassTransit;
using MessageBus.Core.Contracts;
using Order.Application.Repositories;
using Order.Domain.Entities;

namespace Order.Api.Consumers;

public class CartCheckoutConsumer : IConsumer<CartCheckoutEvent>
{
    private readonly IOrderRepository _orderRepository;

    public CartCheckoutConsumer(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Consume(ConsumeContext<CartCheckoutEvent> context)
    {
        var order = new ShoppingOrder { UserId = context.Message.UserId };
        await _orderRepository.InsertAsync(order);
    }
}
