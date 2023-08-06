namespace Order.Domain.Entities;

public class EntityBase
{
    public string Id { get; set; } = null!;
    public string CreatorUserId { get; set; } = null!;
    public DateTime CreationTime { get; set; }
    public string LastModifierUserId { get; set; } = null!;
    public DateTime LastModificationTime { get; set; }
}
