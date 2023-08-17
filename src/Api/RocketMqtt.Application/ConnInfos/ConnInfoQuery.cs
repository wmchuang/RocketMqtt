using Mapster;
using RocketMqtt.Application.Common;
using RocketMqtt.Application.ConnInfos.Request;
using RocketMqtt.Application.ConnInfos.Result;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Util.Model;
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

    public async Task<PageListResult<ConnInfoResult>> GetPageListAsync(ConnInfoPageRequest request)
    {
        return await _baseDbClient.Queryable<ConnInfo>()
            .WhereIF(!string.IsNullOrWhiteSpace(request.ClientId), x => x.ClientId == request.ClientId)
            .WhereIF(!string.IsNullOrWhiteSpace(request.UserName), x => x.UserName.Contains(request.UserName))
            .ToPagedListAsync<ConnInfo,ConnInfoResult>(request.PageIndex, request.PageSize);
    }
}