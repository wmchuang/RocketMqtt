using RocketMqtt.Application.Common;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Web.Core.Results;
using SqlSugar;

namespace RocketMqtt.Application;

public class ConnInfoService : IConnInfoService
{
    private readonly SqlSugarScopeProvider _baseDbClient;

    public ConnInfoService(BaseDbClient baseDbClient)
    {
        _baseDbClient = baseDbClient.Db;
    }

    public async Task AddAsync(ConnInfo connInfo)
    {
        await _baseDbClient.Insertable(connInfo).ExecuteCommandAsync();
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