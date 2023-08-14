using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Application.ConnInfos.Command;

public class DeleteConnInfoCommandHandler : IRequestHandler<DeleteConnInfoCommand, bool>
{
    private readonly IRepository<ConnInfo> _connInfoRep;

    public DeleteConnInfoCommandHandler(IRepository<ConnInfo> connInfoRep)
    {
        _connInfoRep = connInfoRep;
    }

    public async Task<bool> Handle(DeleteConnInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _connInfoRep.FirstOrDefaultAsync(x => x.ClientId == request.ClientId);

        await _connInfoRep.DeleteAsync(entity);
        await _connInfoRep.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return true;
    }
}