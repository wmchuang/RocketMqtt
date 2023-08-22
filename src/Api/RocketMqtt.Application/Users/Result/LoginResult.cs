namespace RocketMqtt.Application.Users.Result;

public class LoginResult
{
    /// <summary>
    /// 授权token
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// 用户Id
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;
}