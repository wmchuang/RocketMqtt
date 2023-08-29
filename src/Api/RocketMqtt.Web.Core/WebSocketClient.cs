using System.Collections.Concurrent;
using MQTTnet;
using MQTTnet.Client;
using RocketMqtt.Application.WebSocket.Request;

namespace RocketMqtt.Web.Core;

public class WebSocketClient
{
    /// <summary>
    /// 
    /// </summary>
    public ConcurrentDictionary<string, IMqttClient> MqttClients = new();

    private readonly MqttFactory _mqttFactory;
    
    
    /// <summary>
    /// ctor
    /// </summary>
    public WebSocketClient()
    {
        _mqttFactory =new MqttFactory();
    }

    /// <summary>
    /// 创建Client
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<MqttClientConnectResult> CreateClient(WebSocketConnRequest request)
    {
        // WebSocket 
        var options = new MqttClientOptionsBuilder()
            .WithClientId(request.ClientId)
            //Protocol does not need to be included in parameter
            .WithWebSocketServer(request.Url)
            //as app.UseHttpsRedirection() is applied in Server, WithTls must be apply
            .WithTls()
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(request.KeepAlive))
            .WithCleanSession()
            .Build();
        var mqttClient = _mqttFactory.CreateMqttClient();
        var response = await mqttClient.ConnectAsync(options, CancellationToken.None);
        
        MqttClients.TryAdd(request.ClientId, mqttClient);
        
        return response;
    }
}