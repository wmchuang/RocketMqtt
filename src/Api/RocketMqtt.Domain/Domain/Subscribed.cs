using RocketMqtt.Domain.Event;

namespace RocketMqtt.Domain.Domain;

public class Subscribed : EntityBase
{
    public string ClientId { get; set; }

    public string TopicName { get; set; }

    public int Qos { get; set; }

    public Subscribed()
    {
    }

    public Subscribed(string clientId, string topicName, int qos) : this()
    {
        ClientId = clientId;
        TopicName = topicName;
        Qos = qos;

        ClientSubscribed(topicName);
    }

    private void ClientSubscribed(string topicName)
    {
        var @event = new ClientSubscribedEvent(topicName);
        this.AddDomainEvent(@event);
    }

    public void UnSubscribed()
    {
        var @event = new ClientUnsubscribedEvent(this.TopicName);
        this.AddDomainEvent(@event);
    }
}