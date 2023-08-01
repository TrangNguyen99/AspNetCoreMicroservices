using Discount.Api.Data;
using Discount.Api.Dto;
using Discount.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Discount.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponsController : ControllerBase
    {
        private readonly DiscountDbContext _discountDbContext;

        public CouponsController(DiscountDbContext discountDbContext)
        {
            _discountDbContext = discountDbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetCoupons()
        {
            var coupons = await _discountDbContext.Coupons.ToListAsync();
            return Ok(coupons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Coupon?>> GetCouponById(Guid id)
        {
            var coupon = await _discountDbContext.Coupons.FindAsync(id);

            if (coupon == null)
            {
                return NotFound();
            }

            return Ok(coupon);
        }

        [HttpPost]
        public async Task<ActionResult<Coupon>> CreateCoupon([FromBody] CreateOrUpdateCouponInput input)
        {
            var coupon = new Coupon
            {
                ProductId = input.ProductId,
                Amount = input.Amount
            };

            _discountDbContext.Coupons.Add(coupon);

            await _discountDbContext.SaveChangesAsync();

            return Ok(coupon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Coupon>> UpdateCoupon(Guid id, [FromBody] CreateOrUpdateCouponInput input)
        {
            var coupon = await _discountDbContext.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            coupon.ProductId = input.ProductId;
            coupon.Amount = input.Amount;

            await _discountDbContext.SaveChangesAsync();

            return Ok(coupon);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCoupon(Guid id)
        {
            var coupon = await _discountDbContext.Coupons.FindAsync(id);
            if (coupon == null)
            {
                return NotFound();
            }

            _discountDbContext.Coupons.Remove(coupon);

            await _discountDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
