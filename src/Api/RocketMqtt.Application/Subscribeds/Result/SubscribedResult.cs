namespace RocketMqtt.Application.Subscribeds.Result;

public class SubscribedResult
{
    public string ClientId { get; set; }

    public string TopicName { get; set; }

    public int Qps { get; set; }
}