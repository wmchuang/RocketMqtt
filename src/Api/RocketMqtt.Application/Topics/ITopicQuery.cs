using RocketMqtt.Application.Common;
using RocketMqtt.Application.Topics.Request;
using RocketMqtt.Application.Topics.Result;

namespace RocketMqtt.Application.Topics;

public interface ITopicQuery
{
    Task<PageListResult<TopicResult>> GetPageListAsync(TopicPageRequest request);
}