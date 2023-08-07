namespace Order.Domain.Entities;

public interface IAuditedEntity<TPrimaryKey, TUserId> : IEntity<TPrimaryKey>
{
    public DateTime CreationTime { get; set; }
    public TUserId? CreatorUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public TUserId? LastModifierUserId { get; set; }
}
