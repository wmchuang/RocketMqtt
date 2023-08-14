using System.Net;
using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Event;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Application.DomainEventHandlers;

public class CreateSubscribedEventHandler : INotificationHandler<CreateSubscribedEvent>
{
    private readonly IRepository<Topic> _topicRep;

    public CreateSubscribedEventHandler(IRepository<Topic> topicRep)
    {
        _topicRep = topicRep;
    }

    public async Task Handle(CreateSubscribedEvent notification, CancellationToken cancellationToken)
    {
        string ipAddress = string.Empty;
        // 使用 Dns.GetHostName 方法获取本地主机名称
        string hostName = Dns.GetHostName();

        // 使用 Dns.GetHostEntry 方法获取本地主机地址信息
        IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

        // 选择 IPv4 地址
        var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        if (ipv4Address != null)
        {
            // 将 IP 地址转换为字符串形式
            ipAddress = ipv4Address.ToString();
        }

        await _topicRep.AddAsync(new Topic(notification.TopicName, ipAddress));

        await _topicRep.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}