using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order.Application.Repositories;
using Order.Domain.Entities;

namespace Order.Infrastructure.Repositories;

public class EfcoreRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
{
    private readonly DbContext _dbContext;

    public EfcoreRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TEntity?> GetAsync(TPrimaryKey id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public async Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _dbContext.Set<TEntity>().AddAsync(entity);
        return entity;
    }
}
