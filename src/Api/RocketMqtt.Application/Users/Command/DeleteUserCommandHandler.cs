using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Util.Exception;

namespace RocketMqtt.Application.Users.Command;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IRepository<User> _userRep;

    public DeleteUserCommandHandler(IRepository<User> userRep)
    {
        _userRep = userRep;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRep.FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user == null)
        {
            throw new OperationException("错误的用户标识");
        }

        await _userRep.DeleteAsync(user);

        await _userRep.SaveChangesAsync(cancellationToken);

        return true;
    }
}