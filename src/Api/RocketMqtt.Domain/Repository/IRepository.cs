using System.Linq.Expressions;

namespace RocketMqtt.Domain.Repository;

/// <summary>
/// 领域泛型仓储
/// </summary>
public interface IRepository<TDomain> where TDomain : class
{
    IUnitOfWork UnitOfWork { get; }
    
    #region Select

    /// <summary>
    /// Gets an entity with given primary key.
    /// </summary>
    /// <param name="id">Primary key of the entity to get</param>
    /// <returns>Entity</returns>
    Task<TDomain?> GetAsync(string id);

    Task<List<TDomain>> GetListAsync(Expression<Func<TDomain, bool>>? predicate = default);

    /// <summary>
    /// Gets an entity with given given predicate or null if not found.
    /// </summary>
    /// <param name="predicate">Predicate to filter entities</param>
    Task<TDomain> FirstOrDefaultAsync(Expression<Func<TDomain, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<TDomain, bool>> predicate);

    #endregion

    #region Insert

    /// <summary>
    /// Inserts a new entity.
    /// </summary>
    /// <param name="entity">Inserted entity</param>
    Task AddAsync(TDomain entity);

    /// <summary>
    /// 批量添加
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task AddRangeAsync(IEnumerable<TDomain> entity);

    #endregion Insert

    #region Update

    /// <summary>
    /// Updates an existing entity. 
    /// </summary>
    /// <param name="entity">Entity</param>
    Task UpdateAsync(TDomain entity);

    Task UpdateRangeAsync(IEnumerable<TDomain> entity);

    #endregion Update

    #region Delete

    /// <summary>
    /// Deletes an entity.
    /// </summary>
    /// <param name="entity">Entity to be deleted</param>
    Task DeleteAsync(TDomain entity);

    /// <summary>
    /// Deletes an entity by primary key.
    /// </summary>
    /// <param name="id">Primary key of the entity</param>
    Task DeleteAsync(string id);

    /// <summary>
    /// Deletes many entities by function.
    /// Notice that: All entities fits to given predicate are retrieved and deleted.
    /// This may cause major performance problems if there are too many entities with
    /// given predicate.
    /// </summary>
    /// <param name="predicate">A condition to filter entities</param>
    Task DeleteAsync(Expression<Func<TDomain, bool>> predicate);

    Task DeleteRangeAsync(Expression<Func<TDomain, bool>> predicate);

    #endregion
}