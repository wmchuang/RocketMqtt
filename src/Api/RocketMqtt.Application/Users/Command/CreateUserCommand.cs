using MediatR;

namespace RocketMqtt.Application.Users.Command;

public class CreateUserCommand : IRequest<bool>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remak { get; set; }
}