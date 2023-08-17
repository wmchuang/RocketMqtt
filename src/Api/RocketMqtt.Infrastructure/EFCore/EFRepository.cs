using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Infrastructure.EFCore;

public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly DataContext _context;

    public IUnitOfWork UnitOfWork
    {
        get { return _context; }
    }

    public EFRepository(DataContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TEntity?> GetAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = default)
    {
        if (predicate == null)
        {
            return null;
        }

        return await _context.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            return null;
        }

        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            return false;
        }

        return await _context.Set<TEntity>().AnyAsync(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entity)
    {
        await _context.Set<TEntity>().AddRangeAsync(entity);
    }

    public Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        return Task.CompletedTask;
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entity)
    {
        _context.Set<TEntity>().UpdateRange(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return;
        }

        var data = await GetAsync(id);
        if (data == null)
        {
            return;
        }

        await DeleteAsync(data);
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            return;
        }

        var data = await FirstOrDefaultAsync(predicate);
        if (data == null)
        {
            return;
        }

        await DeleteAsync(data);
    }

    public async Task DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            return;
        }

        var data = await GetListAsync(predicate);
        if (data == null)
        {
            return;
        }

        _context.Set<TEntity>().RemoveRange(data);
    }
}