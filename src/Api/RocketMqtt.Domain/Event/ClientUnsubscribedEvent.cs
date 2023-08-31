namespace RocketMqtt.Domain.Event;

public class ClientUnsubscribedEvent : IDomainEvent
{
    /// <summary>
    /// 客户端Id
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// 主题
    /// </summary>
    public string TopicName { get; set; }

    public ClientUnsubscribedEvent(string clientId, string topicName)
    {
        ClientId = clientId;
        TopicName = topicName;
    }
}