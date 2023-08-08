using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts;
using Order.Domain.Entities;

namespace Order.Infrastructure.Data;

public abstract class CustomDbContext : DbContext
{
    private readonly ICurrentUserProvider<string> _currentUserProvider;

    public CustomDbContext(DbContextOptions options, ICurrentUserProvider<string> currentUserProvider) : base(options)
    {
        _currentUserProvider = currentUserProvider;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var userId = _currentUserProvider.GetCurrentUserId();

        var addedEntities = ChangeTracker.Entries<AuditedEntity>()
            .Where(e => e.State == EntityState.Added);

        foreach (var addedEntity in addedEntities)
        {
            addedEntity.Entity.CreationTime = now;
            addedEntity.Entity.CreatorUserId = userId;
        }

        var modifiedEntities = ChangeTracker.Entries<AuditedEntity>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var modifiedEntity in modifiedEntities)
        {
            modifiedEntity.Entity.LastModificationTime = now;
            modifiedEntity.Entity.LastModifierUserId = userId;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
