using RocketMqtt.Domain.Event;

namespace RocketMqtt.Domain.Domain;

public class Subscribed : EntityBase
{
    public string ClientId { get; set; }

    public string TopicName { get; set; }

    public int Qps { get; set; }

    public Subscribed()
    {
    }

    public Subscribed(string clientId, string topicName, int qps) : this()
    {
        ClientId = clientId;
        TopicName = topicName;
        Qps = qps;

        AddCreateDomainEvent(topicName);
    }

    private void AddCreateDomainEvent(string topicName)
    {
        var orderStartedDomainEvent = new CreateSubscribedEvent(topicName);

        this.AddDomainEvent(orderStartedDomainEvent);
    }
}