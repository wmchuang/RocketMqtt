using RocketMqtt.Application.Common;
using RocketMqtt.Application.Topics.Request;
using RocketMqtt.Application.Topics.Result;
using RocketMqtt.Util.Model;

namespace RocketMqtt.Application.Topics;

public interface ITopicQuery
{
    Task<PageListResult<TopicResult>> GetPageListAsync(TopicPageRequest request);
}