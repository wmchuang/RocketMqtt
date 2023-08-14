using MediatR;

namespace RocketMqtt.Domain.Event;

public class ClientUnsubscribedEvent : INotification
{
    public string TopicName { get; set; }

    public ClientUnsubscribedEvent(string topicName)
    {
        TopicName = topicName;
    }
}