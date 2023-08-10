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
        await _connInfoRep.AddAsync(new ConnInfo()
        {
            ClientId = request.ClientId,
            UserName = request.UserName,
            Endpoint = request.Endpoint,
            KeepAlive = request.KeepAlive
        });
        return true;
    }
}