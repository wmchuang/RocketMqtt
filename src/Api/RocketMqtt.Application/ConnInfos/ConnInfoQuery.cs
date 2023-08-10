using RocketMqtt.Application.Common;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Web.Core.Results;
using SqlSugar;

namespace RocketMqtt.Application.ConnInfos;

public class ConnInfoQuery : IConnInfoQuery
{
    private readonly SqlSugarScopeProvider _baseDbClient;

    public ConnInfoQuery(BaseDbClient baseDbClient)
    {
        _baseDbClient = baseDbClient.Db;
    }

    public async Task<List<ConnInfo>> GetListAsync()
    {
        return await _baseDbClient.Queryable<ConnInfo>().ToListAsync();
    }

    public async Task<PageListResult<ConnInfo>> GetPageListAsync(BasePageRequest request)
    {
        return await _baseDbClient.Queryable<ConnInfo>().ToPagedListAsync(request.PageIndex, request.PageSize);
    }
}