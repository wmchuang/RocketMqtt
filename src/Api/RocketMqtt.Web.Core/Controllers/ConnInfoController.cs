using Microsoft.AspNetCore.Mvc;
using RocketMqtt.Application;
using RocketMqtt.Application.Common;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 连接信息
/// </summary>
public class ConnInfoController : BaseController
{
    private readonly IConnInfoService _connInfoService;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="connInfoService"></param>
    public ConnInfoController(IConnInfoService connInfoService)
    {
        _connInfoService = connInfoService;
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
        return _connInfoService.GetListAsync();
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<PageListResult<ConnInfo>> PageListAsync(BasePageRequest request)
    {
        return _connInfoService.GetPageListAsync(request);
    }
}