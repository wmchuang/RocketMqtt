using MediatR;

namespace RocketMqtt.Application.Subscribeds.Command;

public class CreateSubscribedCommand : IRequest<bool>
{
    public string ClientId { get; set; } = string.Empty;

    public string TopicName { get; set; } = string.Empty;

    public int Qps { get; set; }
}