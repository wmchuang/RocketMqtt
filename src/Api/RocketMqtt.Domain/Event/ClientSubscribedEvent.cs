using MediatR;

namespace RocketMqtt.Domain.Event;

public class ClientSubscribedEvent : INotification
{
    public string TopicName { get; set; }

    public ClientSubscribedEvent(string topicName)
    {
        TopicName = topicName;
    }
}