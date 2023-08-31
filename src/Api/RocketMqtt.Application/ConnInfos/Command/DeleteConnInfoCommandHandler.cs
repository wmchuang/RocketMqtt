using MediatR;
using RocketMqtt.Application.Subscribeds.Command;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Application.ConnInfos.Command;

public class DeleteConnInfoCommandHandler : IRequestHandler<DeleteConnInfoCommand, bool>
{
    private readonly IRepository<ConnInfo> _connInfoRep;
    private readonly IMediator _mediator;

    public DeleteConnInfoCommandHandler(IRepository<ConnInfo> connInfoRep,IMediator mediator)
    {
        _connInfoRep = connInfoRep;
        _mediator = mediator;
    }

    public async Task<bool> Handle(DeleteConnInfoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _connInfoRep.FirstOrDefaultAsync(x => x.ClientId == request.ClientId);

        await _connInfoRep.DeleteAsync(entity);
        await _connInfoRep.SaveChangesAsync(cancellationToken);

        
        var command = new ClientUnsubscribedCommand()
        {
            ClientId = entity.ClientId,
            TopicName = "",
        };
        await _mediator.Send(command, cancellationToken);
        return true;
    }
}