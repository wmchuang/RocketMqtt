using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Application;

using Domain.Repository;

public class ConnInfoService : IConnInfoService
{
    private readonly IDomainRepository<ConnInfo> _connInfoRep;

    public ConnInfoService(IDomainRepository<ConnInfo> connInfoRep)
    {
        _connInfoRep = connInfoRep;
    }

    public async Task AddAsync(ConnInfo connInfo)
    {
        await _connInfoRep.AddAsync(connInfo);
    }
    
    public async Task<List<ConnInfo>> GetListAsync()
    {
        return await _connInfoRep.GetListAsync(null);
    }
}