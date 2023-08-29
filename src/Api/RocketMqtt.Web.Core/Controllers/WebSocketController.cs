using Microsoft.AspNetCore.Mvc;
using MQTTnet;
using MQTTnet.Client;
using RocketMqtt.Application.WebSocket.Request;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// websocket
/// </summary>
public class WebSocketController : BaseController
{
    private readonly WebSocketClient _webSocketClient;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="webSocketClient"></param>
    public WebSocketController(WebSocketClient webSocketClient)
    {
        _webSocketClient = webSocketClient;
    }

    /// <summary>
    /// 连接
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<MqttClientConnectResult> ConnectAsync(WebSocketConnRequest request)
    {
        return await _webSocketClient.CreateClient(request);
    }
}