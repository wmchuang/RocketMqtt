using RocketMqtt.Application.Common;
using RocketMqtt.Application.ConnInfos.Request;
using RocketMqtt.Application.ConnInfos.Result;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Application.ConnInfos;

public interface IConnInfoQuery
{
    Task<List<ConnInfo>> GetListAsync();
    Task<PageListResult<ConnInfoResult>> GetPageListAsync(ConnInfoPageRequest request);
}