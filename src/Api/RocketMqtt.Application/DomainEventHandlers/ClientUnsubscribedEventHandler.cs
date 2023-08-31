using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Event;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Application.DomainEventHandlers;

public class ClientUnsubscribedEventHandler : INotificationHandler<ClientUnsubscribedEvent>
{
    private readonly IRepository<Topic> _topicRep;
    private readonly IRepository<Subscribed> _subscribedRep;

    public ClientUnsubscribedEventHandler(IRepository<Topic> topicRep, IRepository<Subscribed> subscribedRep)
    {
        _topicRep = topicRep;
        _subscribedRep = subscribedRep;
    }

    public async Task Handle(ClientUnsubscribedEvent notification, CancellationToken cancellationToken)
    {
        var exist = await _subscribedRep.AnyAsync(x => x.ClientId != notification.ClientId && x.TopicName == notification.TopicName);
        if (exist) return;

        var entity = await _topicRep.FirstOrDefaultAsync(x => x.TopicName == notification.TopicName);

        await _topicRep.DeleteAsync(entity);
        await _topicRep.SaveChangesAsync(cancellationToken);
    }
}

public class ClientUnsubscribedEventCutSubCountHandler : INotificationHandler<ClientUnsubscribedEvent>
{
    private readonly IRepository<ConnInfo> _connInfoRep;

    public ClientUnsubscribedEventCutSubCountHandler(IRepository<ConnInfo> connInfoRep)
    {
        _connInfoRep = connInfoRep;
    }

    public async Task Handle(ClientUnsubscribedEvent notification, CancellationToken cancellationToken)
    {
        var entity = await _connInfoRep.FirstOrDefaultAsync(x => x.ClientId == notification.ClientId);
        if (entity == null) return;

        entity.CutSubscribeCount();

        await _connInfoRep.UpdateAsync(entity);

        await _connInfoRep.SaveChangesAsync(cancellationToken);
    }
}