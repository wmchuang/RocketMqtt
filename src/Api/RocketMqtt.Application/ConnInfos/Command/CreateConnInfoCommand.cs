using MediatR;

namespace RocketMqtt.Application.ConnInfos.Command;

public class CreateConnInfoCommand : IRequest<bool>
{
    /// <summary>
    /// 客户端Id
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 地址
    /// </summary>
    public string Endpoint1 { get; set; } = string.Empty;

    /// <summary>
    /// 心跳（秒）
    /// </summary>
    public uint KeepAlive { get; set; }

 
}