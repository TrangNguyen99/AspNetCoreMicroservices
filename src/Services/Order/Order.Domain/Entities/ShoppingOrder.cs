namespace Order.Domain.Entities;

public class ShoppingOrder : AuditedEntity
{
    public string UserId { get; set; } = null!;
}
