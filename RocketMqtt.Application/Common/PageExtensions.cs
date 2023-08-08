using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Application.Common;

public static class PageExtensions
{
    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <param name="entities"></param>
    /// <param name="pageIndex">页码，必须大于0</param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static PageListResult<TEntity> ToPageList<TEntity>(this List<TEntity> entities,int pageIndex = 1, int pageSize = 20)
    {
        if (pageIndex <= 0)
            throw new InvalidOperationException($"{nameof(pageIndex)} must be a positive integer greater than 0.");

        var totalCount = entities.Count();
        var items = entities.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PageListResult<TEntity>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNextPages = pageIndex < totalPages,
            HasPrevPages = pageIndex - 1 > 0
        };
    }
}