using RocketMqtt.Application.Common;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Application.ConnInfos;

public interface IConnInfoQuery
{
    Task<List<ConnInfo>> GetListAsync();
    Task<PageListResult<ConnInfo>> GetPageListAsync(BasePageRequest request);
}