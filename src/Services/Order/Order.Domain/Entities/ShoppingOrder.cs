namespace Order.Domain.Entities;

public class ShoppingOrder : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = null!;
}
