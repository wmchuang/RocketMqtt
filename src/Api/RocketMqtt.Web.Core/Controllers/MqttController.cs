using System.Net;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet.Server;
using RocketMqtt.Application.ConnInfos.Command;
using RocketMqtt.Application.Subscribeds.Command;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// Mqtt
/// </summary>
public class MqttController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="serviceProvider"></param>
    public MqttController(IServiceProvider serviceProvider)
    {
        _mediator = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IMediator>();
    }

    /// <summary>
    /// 客户端已连接
    /// </summary>
    /// <param name="eventArgs"></param>
    /// <returns></returns>
    public async Task OnClientConnected(ClientConnectedEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' connected.");

        // 解析 IP 地址和端口号
        var endPoint = IPEndPoint.Parse(eventArgs.Endpoint);

        //转换IPv6的地址到IPv4
        if (endPoint.Address.IsIPv4MappedToIPv6)
        {
            endPoint.Address = endPoint.Address.MapToIPv4();
        }

        var command = new CreateConnInfoCommand()
        {
            ClientId = eventArgs.ClientId,
            UserName = eventArgs.UserName,
            Endpoint = endPoint.ToString()
        };
        await _mediator.Send(command);
    }

    /// <summary>
    /// 验证连接
    /// </summary>
    /// <param name="eventArgs"></param>
    /// <returns></returns>
    public Task ValidateConnection(ValidatingConnectionEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' wants to connect. Accepting!");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 客户端已断开连接
    /// </summary>
    /// <param name="eventArgs"></param>
    /// <returns></returns>
    public async Task ClientDisconnectedAsync(ClientDisconnectedEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' wants to Disconnected!");

        var command = new DeleteConnInfoCommand()
        {
            ClientId = eventArgs.ClientId,
        };
        await _mediator.Send(command);
    }

    /// <summary>
    /// 客户端订阅主题
    /// </summary>
    /// <param name="eventArgs"></param>
    /// <returns></returns>
    public async Task ClientSubscribedTopicAsync(ClientSubscribedTopicEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' wants to ClientSubscribedTopic!");

        var command = new CreateSubscribedCommand()
        {
            ClientId = eventArgs.ClientId,
            TopicName = eventArgs.TopicFilter.Topic,
            Qps = (int)eventArgs.TopicFilter.QualityOfServiceLevel,
        };
        await _mediator.Send(command);
    }
}