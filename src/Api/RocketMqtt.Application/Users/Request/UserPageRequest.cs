using RocketMqtt.Util.Model;

namespace RocketMqtt.Application.Users.Request;

public class UserPageRequest : BasePageRequest
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
}