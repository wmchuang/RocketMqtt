using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Application.Subscribeds.Command;

public class CreateSubscribedCommandHandler : IRequestHandler<CreateSubscribedCommand, bool>
{
    private readonly IRepository<Subscribed> _subscribedRep;

    public CreateSubscribedCommandHandler(IRepository<Subscribed> subscribedRep)
    {
        _subscribedRep = subscribedRep;
    }

    public async Task<bool> Handle(CreateSubscribedCommand request, CancellationToken cancellationToken)
    {
        var entity = new Subscribed(request.ClientId,request.TopicName,request.Qos);

        await _subscribedRep.AddAsync(entity);
        await _subscribedRep.SaveChangesAsync(cancellationToken);
        return true;
    }
}