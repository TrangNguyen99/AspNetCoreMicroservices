using Order.Domain.Entities;

namespace Order.Application.Repositories;

public interface IOrderRepository : IRepository<ShoppingOrder, Guid>
{
}
