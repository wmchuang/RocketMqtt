using MediatR;

namespace RocketMqtt.Application.Users.Command;

public class UpdateUserRemarkCommand : IRequest<bool>
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}