using System.Net;
using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Event;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Infrastructure.Extensions;

namespace RocketMqtt.Application.DomainEventHandlers;

public class ClientSubscribedEventHandler : INotificationHandler<ClientSubscribedEvent>
{
    private readonly IRepository<Topic> _topicRep;

    public ClientSubscribedEventHandler(IRepository<Topic> topicRep)
    {
        _topicRep = topicRep;
    }

    public async Task Handle(ClientSubscribedEvent notification, CancellationToken cancellationToken)
    {
        var entity = await _topicRep.FirstOrDefaultAsync(x => x.TopicName == notification.TopicName);
        if (entity != null) return;

        var ipAddress = NetworkExtension.GetIp().ToString();

        await _topicRep.AddAsync(new Topic(notification.TopicName, ipAddress));

        await _topicRep.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}