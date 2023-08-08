namespace RocketMqtt.Application.User.Result;

public class LoginResult
{
    /// <summary>
    /// 授权token
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    public string FullName { get; set; } = string.Empty;
}