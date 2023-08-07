using AutoMapper;
using MediatR;
using Order.Application.Repositories;
using Order.Domain.Entities;

namespace Order.Application.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderCommandOutput>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<CreateOrderCommandOutput> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.InsertAsync(_mapper.Map<ShoppingOrder>(request));
        return _mapper.Map<CreateOrderCommandOutput>(order);
    }
}
