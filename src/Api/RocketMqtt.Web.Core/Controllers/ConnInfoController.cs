using Microsoft.AspNetCore.Mvc;
using RocketMqtt.Application;
using RocketMqtt.Application.Common;
using RocketMqtt.Application.ConnInfos;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 连接信息
/// </summary>
public class ConnInfoController : BaseController
{
    private readonly IConnInfoQuery _connInfoQuery;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="connInfoQuery"></param>
    public ConnInfoController(IConnInfoQuery connInfoQuery)
    {
        _connInfoQuery = connInfoQuery;
    }

    /// <summary>
    /// Test
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public string Hello()
    {
        return "Hello";
    }

    /// <summary>
    /// 列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<List<ConnInfo>> ListAsync()
    {
        return _connInfoQuery.GetListAsync();
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<PageListResult<ConnInfo>> PageListAsync(BasePageRequest request)
    {
        return _connInfoQuery.GetPageListAsync(request);
    }
}