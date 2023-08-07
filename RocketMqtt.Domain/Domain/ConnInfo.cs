namespace RocketMqtt.Domain.Domain;

/// <summary>
/// 连接信息
/// </summary>
public class ConnInfo : EntityBase
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
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// 心跳（秒）
    /// </summary>
    public uint KeepAlive { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; } = DateTime.Now;
}