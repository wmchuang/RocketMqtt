using InterfaceGenerator;
using RocketMqtt.Application.Common;
using RocketMqtt.Application.Subscribeds.Request;
using RocketMqtt.Application.Subscribeds.Result;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Util.Model;
using SqlSugar;

namespace RocketMqtt.Application.Subscribeds;

[GenerateAutoInterface]
public class SubscribedQuery : ISubscribedQuery
{
    private readonly SqlSugarScopeProvider _baseDbClient;

    public SubscribedQuery(BaseDbClient baseDbClient)
    {
        _baseDbClient = baseDbClient.Db;
    }

    public async Task<PageListResult<SubscribedResult>> GetPageListAsync(SubscribedPageRequest request)
    {
        return await _baseDbClient.Queryable<Subscribed>()
            .WhereIF(!string.IsNullOrWhiteSpace(request.TopicName), x => x.TopicName.Contains(request.TopicName))
            .WhereIF(!string.IsNullOrWhiteSpace(request.ClientId), x => x.ClientId.Contains(request.ClientId))
            .ToPagedListAsync<Subscribed, SubscribedResult>(request.PageIndex, request.PageSize);
    }
}