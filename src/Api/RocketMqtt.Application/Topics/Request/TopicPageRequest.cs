using RocketMqtt.Application.Common;

namespace RocketMqtt.Application.Topics.Request;

public class TopicPageRequest : BasePageRequest
{
    public string TopicName { get; set; }
}