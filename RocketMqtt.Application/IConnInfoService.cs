using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Application;

public interface IConnInfoService
{
    Task AddAsync(ConnInfo connInfo);
    Task<List<ConnInfo>> GetListAsync();
}