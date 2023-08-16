using MediatR;

namespace RocketMqtt.Domain.Event;

public class ClientSubscribedEvent : INotification
{
    /// <summary>
    /// 客户端Id
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// 主题
    /// </summary>
    public string TopicName { get; set; }

    public ClientSubscribedEvent(string clientId, string topicName)
    {
        ClientId = clientId;
        TopicName = topicName;
    }
}