using Microsoft.AspNetCore.Mvc;
using RocketMqtt.Application;
using RocketMqtt.Domain.Domain;

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
    /// 列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public string Hello()
    {
        throw new Exception();

        return "Hello";
    }

    /// <summary>
    /// 列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<List<ConnInfo>> List()
    {
        return _connInfoService.GetListAsync();
    }
}