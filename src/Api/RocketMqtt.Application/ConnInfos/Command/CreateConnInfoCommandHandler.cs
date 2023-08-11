using Mapster;
using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Application.ConnInfos.Command;

public class CreateConnInfoCommandHandler : IRequestHandler<CreateConnInfoCommand, bool>
{
    private readonly IDomainRepository<ConnInfo> _connInfoRep;

    public CreateConnInfoCommandHandler(IDomainRepository<ConnInfo> connInfoRep)
    {
        _connInfoRep = connInfoRep;
    }

    public async Task<bool> Handle(CreateConnInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<ConnInfo>();

        await _connInfoRep.AddAsync(entity);

        return true;
    }
}