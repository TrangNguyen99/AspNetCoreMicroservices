namespace Order.Domain.Entities;

public class AuditedEntity : IAuditedEntity<Guid, string>
{
    public Guid Id { get; set; }
    public DateTime CreationTime { get; set; }
    public string? CreatorUserId { get; set; }
    public DateTime? LastModificationTime { get; set; }
    public string? LastModifierUserId { get; set; }
}
