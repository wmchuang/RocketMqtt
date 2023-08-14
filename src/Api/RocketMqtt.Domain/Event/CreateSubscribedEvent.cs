using MediatR;

namespace RocketMqtt.Domain.Event;

public class CreateSubscribedEvent : INotification
{
    public string TopicName { get; set; }

    public CreateSubscribedEvent(string topicName)
    {
        TopicName = topicName;
    }
}