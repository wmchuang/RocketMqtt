using RocketMqtt.Application.Common;

namespace RocketMqtt.Application.ConnInfos.Request;

public class ConnInfoPageRequest : BasePageRequest
{
    /// <summary>
    /// 客户端Id
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// 客户端用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;
}