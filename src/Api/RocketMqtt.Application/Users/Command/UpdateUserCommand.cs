using MediatR;

namespace RocketMqtt.Application.Users.Command;

public class UpdateUserCommand : IRequest<bool>
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 旧密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 新密码
    /// </summary>
    public string NewPassword { get; set; }

    /// <summary>
    /// 确认新密码
    /// </summary>
    public string ConfirmPassword { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}