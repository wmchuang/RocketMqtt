using RocketMqtt.Application.Common;
using RocketMqtt.Application.Topics.Request;
using RocketMqtt.Application.Topics.Result;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Util.Model;
using SqlSugar;

namespace RocketMqtt.Application.Topics;

public class TopicQuery : ITopicQuery
{
    private readonly SqlSugarScopeProvider _baseDbClient;

    public TopicQuery(BaseDbClient baseDbClient)
    {
        _baseDbClient = baseDbClient.Db;
    }

    public async Task<PageListResult<TopicResult>> GetPageListAsync(TopicPageRequest request)
    {
        return await _baseDbClient.Queryable<Topic>()
            .WhereIF(!string.IsNullOrWhiteSpace(request.TopicName), x => x.TopicName.Contains(request.TopicName))
            .ToPagedListAsync<Topic, TopicResult>(request.PageIndex, request.PageSize);
    }
}