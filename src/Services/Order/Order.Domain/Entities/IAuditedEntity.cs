namespace Order.Domain.Entities;

public interface IAuditedEntity<TPrimaryKey, TUserId> : IEntity<TPrimaryKey>
{
    DateTime CreationTime { get; set; }
    TUserId? CreatorUserId { get; set; }
    DateTime? LastModificationTime { get; set; }
    TUserId? LastModifierUserId { get; set; }
}
