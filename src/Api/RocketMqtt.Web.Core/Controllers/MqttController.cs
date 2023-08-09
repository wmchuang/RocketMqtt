﻿using Microsoft.Extensions.DependencyInjection;
using MQTTnet.Server;
using RocketMqtt.Application;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// Mqtt
/// </summary>
public class MqttController
{
    private readonly IServiceProvider _serviceProvider;

    public MqttController(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Client 连接时
    /// </summary>
    /// <param name="eventArgs"></param>
    /// <returns></returns>
    public Task OnClientConnected(ClientConnectedEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' connected.");

        using (var scope = _serviceProvider.CreateScope())
        {
            var _connInfoService = scope.ServiceProvider.GetRequiredService<IConnInfoService>();
            _connInfoService.AddAsync(new ConnInfo()
            {
                ClientId = eventArgs.ClientId,
                UserName = eventArgs.UserName,
                Endpoint = eventArgs.Endpoint
            });
        }

        return Task.CompletedTask;
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
    /// 连接断开
    /// </summary>
    /// <param name="eventArgs"></param>
    /// <returns></returns>
    public Task ClientDisconnectedAsync(ClientDisconnectedEventArgs eventArgs)
    {
        Console.WriteLine($"Client '{eventArgs.ClientId}' wants to Disconnected!");
        return Task.CompletedTask;
    }
}