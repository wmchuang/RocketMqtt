using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;

namespace RocketMqtt.Application.Subscribeds.Command;

public class ClientUnsubscribedCommandHandler : IRequestHandler<ClientUnsubscribedCommand, bool>
{
    private readonly IRepository<Subscribed> _subscribedRep;

    public ClientUnsubscribedCommandHandler(IRepository<Subscribed> subscribedRep)
    {
        _subscribedRep = subscribedRep;
    }

    public async Task<bool> Handle(ClientUnsubscribedCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.TopicName))
        {
            var entitys = await _subscribedRep.GetListAsync(x => x.ClientId == request.ClientId);
            foreach (var entity in entitys)
            {
                entity.UnSubscribed();
                await _subscribedRep.DeleteAsync(entity);
            }
            
            await _subscribedRep.SaveChangesAsync(cancellationToken);
        }
        else
        {
            var entity = await _subscribedRep.FirstOrDefaultAsync(x => x.ClientId == request.ClientId && x.TopicName == request.TopicName);

            if (entity != null)
            {
                entity.UnSubscribed();
                await _subscribedRep.DeleteAsync(entity);
                await _subscribedRep.SaveChangesAsync(cancellationToken);
            }
        }

        return true;
    }
}