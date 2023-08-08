using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands.CreateOrder;
using Order.Application.Queries.GetOrders;

namespace Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetOrdersOutput>> GetOrdersByUserId([FromQuery] string userId)
    {
        var query = new GetOrdersQuery { UserId = userId };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<CreateOrderCommandOutput>> CreateOrder([FromQuery] string userId)
    {
        var command = new CreateOrderCommand { UserId = userId };
        var order = await _mediator.Send(command);
        return Ok(order);
    }
}
