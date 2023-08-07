using System.Linq.Expressions;

namespace Order.Application.Repositories;

public interface IRepository<TEntity, TPrimaryKey>
{
    Task<TEntity?> GetAsync(TPrimaryKey id);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate);

    Task<TEntity> InsertAsync(TEntity entity);
}
