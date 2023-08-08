using RocketMqtt.Application.Common;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Application;

public interface IConnInfoService
{
    Task AddAsync(ConnInfo connInfo);
    Task<List<ConnInfo>> GetListAsync();
    Task<PageListResult<ConnInfo>> GetPageListAsync(BasePageRequest request);
}