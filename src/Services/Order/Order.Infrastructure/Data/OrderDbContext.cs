using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public DbSet<ShoppingOrder> ShoppingOrders { get; set; }

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var addedEntities = ChangeTracker.Entries<IAuditedEntity<Guid, string>>()
            .Where(e => e.State == EntityState.Added);

        foreach (var addedEntity in addedEntities)
        {
            addedEntity.Entity.CreationTime = now;
            addedEntity.Entity.CreatorUserId = "";
        }

        var modifiedEntities = ChangeTracker.Entries<IAuditedEntity<Guid, string>>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var modifiedEntity in modifiedEntities)
        {
            modifiedEntity.Entity.LastModificationTime = now;
            modifiedEntity.Entity.LastModifierUserId = "";
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
