using Discount.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Discount.Api.Data;

public class DiscountDbContext : DbContext
{
    public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options)
    {
    }

    public DbSet<Coupon> Coupons { get; set; } = null!;
}
