using MediatR;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Domain.Repository;
using SqlSugar;

namespace RocketMqtt.Infrastructure.SqlSugar;

public class BaseDbClient : IUnitOfWork
{
    private readonly IMediator _mediator;
    private readonly SugarUnitOfWork uow;

    public List<EntityBase> ModifiedEntities { get; set; } = new();

    public readonly SqlSugarScopeProvider Db;

    public BaseDbClient(ISqlSugarClient db, IMediator mediator)
    {
        _mediator = mediator;
        uow = db.CreateContext();
        Db = uow.Db.AsTenant().GetConnectionScope("RocketMqtt");
    }

    public void Dispose()
    {
        uow.Dispose();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        var domainEvents = ModifiedEntities.SelectMany(x => x.DomainEvents).ToList();
        
        ModifiedEntities.Clear();
        foreach (var domainEvent in domainEvents)
            await _mediator.Publish(domainEvent, cancellationToken);

        
        return uow.Commit() ? 1 : 0;
    }
}