using RocketMqtt.Application.Common;

namespace RocketMqtt.Application.Subscribeds.Request;

public class SubscribedPageRequest : BasePageRequest
{
    public string ClientId { get; set; }

    public string TopicName { get; set; }
}