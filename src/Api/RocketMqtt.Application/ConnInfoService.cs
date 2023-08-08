using RocketMqtt.Application.Common;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Web.Core.Results;

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
        return await _connInfoRep.GetListAsync();
    }

    public async Task<PageListResult<ConnInfo>> GetPageListAsync(BasePageRequest request)
    {
        var list = await _connInfoRep.GetListAsync();
        return list.ToPageList(request.PageIndex, request.PageSize);
    }
}