using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Util.Exception;

namespace RocketMqtt.Application.Users.Command;

public class UpdateUserRemarkCommandHandler : IRequestHandler<UpdateUserRemarkCommand, bool>
{
    private readonly IRepository<User> _userRep;

    public UpdateUserRemarkCommandHandler(IRepository<User> userRep)
    {
        _userRep = userRep;
    }

    public async Task<bool> Handle(UpdateUserRemarkCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRep.FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user == null)
        {
            throw new OperationException("错误的用户标识");
        }

        user.UpdateRemark(request.Remark);
        await _userRep.UpdateAsync(user);

        await _userRep.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}