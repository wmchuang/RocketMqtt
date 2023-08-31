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
            var existEntity = await _connInfoRep.FirstOrDefaultAsync(x => x.ClientId == request.ClientId);
            if (existEntity != null)
            {
                await _connInfoRep.DeleteAsync(existEntity);
            }

            await _connInfoRep.AddAsync(entity);

            await _connInfoRep.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return true;
    }
}