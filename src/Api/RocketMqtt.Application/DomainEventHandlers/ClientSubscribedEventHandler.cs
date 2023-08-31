using System.Net;
using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Event;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Infrastructure.Extensions;

namespace RocketMqtt.Application.DomainEventHandlers;

/// <summary>
/// 客户端订阅事件触发时 处理 主题
/// </summary>
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

        await _topicRep.SaveChangesAsync(cancellationToken);
    }
}

/// <summary>
/// 客户端订阅事件触发时 处理 客户端订阅数量
/// </summary>
public class ClientSubscribedEventAddSubCountHandler : INotificationHandler<ClientSubscribedEvent>
{
    private readonly IRepository<ConnInfo> _connInfoRep;

    public ClientSubscribedEventAddSubCountHandler(IRepository<ConnInfo> connInfoRep)
    {
        _connInfoRep = connInfoRep;
    }

    public async Task Handle(ClientSubscribedEvent notification, CancellationToken cancellationToken)
    {
        var entity = await _connInfoRep.FirstOrDefaultAsync(x => x.ClientId == notification.ClientId);
        if (entity == null) return;

        entity.AddSubscribeCount();

        await _connInfoRep.UpdateAsync(entity);
        
        await _connInfoRep.SaveChangesAsync(cancellationToken);
    }
}