using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data;

public class OrderDbContext : CustomDbContext
{
    public DbSet<ShoppingOrder> Orders { get; set; }

    public OrderDbContext(DbContextOptions<OrderDbContext> options, ICurrentUserProvider<string> currentUserProvider)
        : base(options, currentUserProvider)
    {
    }
}
