using System.Linq.Expressions;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Infrastructure.DbContext;

namespace RocketMqtt.Infrastructure.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private List<TEntity> Entities => (List<TEntity>)(MemoryDbContext.GetInstance().SetEntities<TEntity>());

    public Task AddAsync(TEntity entity)
    {
        Entities.Add(entity);
        return Task.CompletedTask;
    }

    public IUnitOfWork UnitOfWork { get; }

    public async Task<TEntity?> GetAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }
        
        return await Task.Run(() =>
        {
            return Entities.FirstOrDefault(x => x.Id == id);
        });
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate)
    {
        if (predicate == null)
        {
            return Entities;
        }

        return await Task.Run(() => Entities.Where(predicate.Compile()).ToList());
        
    }

    public Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}