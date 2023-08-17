using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Util.Exception;

namespace RocketMqtt.Application.Users.Command;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IRepository<User> _userRep;

    public CreateUserCommandHandler(IRepository<User> userRep)
    {
        _userRep = userRep;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existEntity = await _userRep.FirstOrDefaultAsync(x => x.UserName == request.UserName);
        if (existEntity != null)
        {
            throw new OperationException("已存在的用户名");
        }

        var user = new User(request.UserName, request.Password, request.Remak);
        await _userRep.AddAsync(user);

        await _userRep.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}