using System.Text.Json;
using Cart.Api.Dto;
using Cart.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Cart.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public CartController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
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
            return Ok(cart);
        }

        [HttpPost("{username}")]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(string username, [FromBody] UpdateCartInput input)
        {
            var cart = new ShoppingCart
            {
                Username = username,
                CartItems = input.CartItems
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
}
