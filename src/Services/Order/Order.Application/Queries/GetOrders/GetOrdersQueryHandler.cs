using AutoMapper;
using MediatR;
using Order.Application.Repositories;

namespace Order.Application.Queries.GetOrders;

public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, GetOrdersOutput>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<GetOrdersOutput> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetAllListAsync(x => x.UserId == request.UserId);

        return new GetOrdersOutput
        {
            Orders = _mapper.Map<List<GetOrdersOutput_Order>>(orders),
        };
    }
}
