using RocketMqtt.Application.Common;
using RocketMqtt.Util.Model;

namespace RocketMqtt.Application.Subscribeds.Request;

public class SubscribedPageRequest : BasePageRequest
{
    public string ClientId { get; set; }

    public string TopicName { get; set; }
}