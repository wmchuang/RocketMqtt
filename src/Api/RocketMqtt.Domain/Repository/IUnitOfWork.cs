namespace RocketMqtt.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {        
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
