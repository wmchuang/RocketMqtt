using Mapster;
using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Application.ConnInfos.Command;

public class CreateConnInfoCommandHandler : IRequestHandler<CreateConnInfoCommand, bool>
{
    private readonly IRepository<ConnInfo> _connInfoRep;

    public CreateConnInfoCommandHandler(IRepository<ConnInfo> connInfoRep)
    {
        _connInfoRep = connInfoRep;
    }

    public async Task<bool> Handle(CreateConnInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Adapt<ConnInfo>();

        try
        {
            await _connInfoRep.AddAsync(entity);
            await _connInfoRep.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
     
        return true;
    }
}