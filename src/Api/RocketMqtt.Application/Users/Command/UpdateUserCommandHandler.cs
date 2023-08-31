using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Util.Exception;

namespace RocketMqtt.Application.Users.Command;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
{
    private readonly IRepository<User> _userRep;

    public UpdateUserCommandHandler(IRepository<User> userRep)
    {
        _userRep = userRep;
    }

    public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRep.FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user == null)
        {
            throw new OperationException("错误的用户标识");
        }

        user.UpdateRemark(request.Remark);
        user.UpdatePassword(request.NewPassword, request.ConfirmPassword);
        await _userRep.UpdateAsync(user);

        await _userRep.SaveChangesAsync(cancellationToken);

        return true;
    }
}