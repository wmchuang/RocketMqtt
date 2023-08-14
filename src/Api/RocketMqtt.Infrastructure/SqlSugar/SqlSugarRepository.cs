﻿using System.Linq.Expressions;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;
using SqlSugar;

namespace RocketMqtt.Infrastructure.SqlSugar;

public class SqlSugarRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
{
    private readonly SqlSugarScopeProvider _scopeProvider;
    
    private readonly BaseDbClient _client;

    public SqlSugarRepository(BaseDbClient client)
    {
        _client = client;
        _scopeProvider = client.Db;

        UnitOfWork = client;
    }

    public IUnitOfWork UnitOfWork { get; }

    public async Task<TEntity?> GetAsync(string id)
    {
        return await _scopeProvider.Queryable<TEntity>().FirstAsync(x => x.Id == id);
    }

    public Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = default)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _scopeProvider.Queryable<TEntity>().FirstAsync(predicate);
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _scopeProvider.Insertable(entity).ExecuteCommandAsync();
        _client.ModifiedEntities.Add(entity);
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

    public async Task DeleteAsync(TEntity entity)
    {
        await _scopeProvider.Deleteable(entity).ExecuteCommandAsync();
        _client.ModifiedEntities.Add(entity);
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