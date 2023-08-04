using System.Text.Json;
using Cart.Api.Dto;
using Cart.Api.Entities;
using Catalog.Grpc.Protos;
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

    public CartController(IDistributedCache distributedCache, ProductGrpcServiceClient productGrpcServiceClient)
    {
        _distributedCache = distributedCache;
        _productGrpcServiceClient = productGrpcServiceClient;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<ShoppingCart>> GetCart(string username)
    {
        var cartJson = await _distributedCache.GetStringAsync(username);
        if (string.IsNullOrEmpty(cartJson))
        {
            return Ok(new ShoppingCart { Username = username });
        }

        var cart = JsonSerializer.Deserialize<ShoppingCart>(cartJson);
        var productIds = cart!.CartItems.Select(x => x.ProductId);

        var request = new GetProductsRequest();
        request.Ids.AddRange(productIds);

        var reply = await _productGrpcServiceClient.GetProductsAsync(request);

        var replyProductIds = reply.Products.Select(x => x.Id);

        cart.CartItems = cart.CartItems.Where(x => replyProductIds.Contains(x.ProductId)).ToList();

        cart.CartItems.ForEach(x =>
        {
            var product = reply.Products.First(y => y.Id == x.ProductId);
            x.ProductName = product.Name;
            x.ProductPrice = product.Price;
        });

        return Ok(cart);
    }

    [HttpPost("{username}")]
    public async Task<ActionResult<ShoppingCart>> UpdateCart(string username, [FromBody] UpdateCartInput input)
    {
        var cart = new ShoppingCart
        {
            Username = username,
            CartItems = input.CartItems.Select(x => new CartItem
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity
            }).ToList()
        };

        var cartJson = JsonSerializer.Serialize(cart);
        await _distributedCache.SetStringAsync(username, cartJson);

        return Ok(cart);
    }

    [HttpDelete("{username}")]
    public async Task<ActionResult> DeleteCart(string username)
    {
        await _distributedCache.RemoveAsync(username);
        return Ok();
    }
}
