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

        ClientSubscribed(clientId, topicName);
    }

    private void ClientSubscribed(string clientId, string topicName)
    {
        var @event = new ClientSubscribedEvent(clientId, topicName);
        this.AddDomainEvent(@event);
    }

    public void UnSubscribed()
    {
        var @event = new ClientUnsubscribedEvent(this.ClientId, this.TopicName);
        this.AddDomainEvent(@event);
    }
}