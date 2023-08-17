using RocketMqtt.Application.Common;
using RocketMqtt.Util.Model;

namespace RocketMqtt.Application.Topics.Request;

public class TopicPageRequest : BasePageRequest
{
    public string TopicName { get; set; }
}