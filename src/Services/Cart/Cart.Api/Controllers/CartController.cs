using System.Text.Json;
using Cart.Api.Dto;
using Cart.Api.Entities;
using Catalog.Grpc.Protos;
using MassTransit;
using MessageBus.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using static Catalog.Grpc.Protos.ProductGrpcService;

namespace Cart.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly IDistributedCache _distributedCache;
    private readonly ProductGrpcServiceClient _productGrpcServiceClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public CartController(
        IDistributedCache distributedCache,
        ProductGrpcServiceClient productGrpcServiceClient,
        IPublishEndpoint publishEndpoint)
    {
        _distributedCache = distributedCache;
        _productGrpcServiceClient = productGrpcServiceClient;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<GetCartOutput>> GetCart([FromQuery] string userId)
    {
        var cartJson = await _distributedCache.GetStringAsync(userId);
        if (string.IsNullOrEmpty(cartJson))
        {
            return Ok(new GetCartOutput { CartItems = new() });
        }

        var cart = JsonSerializer.Deserialize<ShoppingCart>(cartJson);
        var productIds = cart!.CartItems.Select(x => x.ProductId);

        var request = new GetProductsRequest();
        request.Ids.AddRange(productIds);

        var reply = await _productGrpcServiceClient.GetProductsAsync(request);

        var replyProductIds = reply.Products.Select(x => x.Id);

        cart.CartItems = cart.CartItems.Where(x => replyProductIds.Contains(x.ProductId)).ToList();

        var resCart = new GetCartOutput
        {
            CartItems = cart.CartItems.Select(x =>
            {
                var product = reply.Products.First(y => y.Id == x.ProductId);
                return new GetCartOutputCartItem
                {
                    ProductId = x.ProductId,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    Quantity = x.Quantity
                };
            }).ToList()
        };

        return Ok(resCart);
    }

    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateCart([FromQuery] string userId, [FromBody] UpdateCartInput input)
    {
        var cart = new ShoppingCart
        {
            UserId = userId,
            CartItems = input.CartItems.Select(x => new CartItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList()
        };

        var cartJson = JsonSerializer.Serialize(cart);
        await _distributedCache.SetStringAsync(userId, cartJson);

        return Ok(cart);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteCart([FromQuery] string userId)
    {
        await _distributedCache.RemoveAsync(userId);
        return Ok();
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout([FromQuery] string userId)
    {
        var message = new CartCheckoutEvent
        {
            UserId = userId,
            CartItems = new()
        };

        await _publishEndpoint.Publish(message);

        return Accepted();
    }
}
