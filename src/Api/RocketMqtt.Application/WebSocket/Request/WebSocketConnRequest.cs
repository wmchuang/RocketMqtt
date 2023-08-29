namespace RocketMqtt.Application.WebSocket.Request;

public class WebSocketConnRequest
{
    public string ClientId { get; set; }
    
    public string Url { get; set; }

    /// <summary>
    /// 心跳（秒）
    /// </summary>
    public uint KeepAlive { get; set; } = 15;
}