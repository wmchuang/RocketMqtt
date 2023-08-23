using MediatR;

namespace RocketMqtt.Application.Users.Command;

public class DeleteUserCommand : IRequest<bool>
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public string UserId { get; set; }
}