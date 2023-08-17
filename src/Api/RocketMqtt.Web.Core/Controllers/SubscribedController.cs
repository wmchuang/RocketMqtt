using Microsoft.AspNetCore.Mvc;
using RocketMqtt.Application.Common;
using RocketMqtt.Application.Subscribeds;
using RocketMqtt.Application.Subscribeds.Request;
using RocketMqtt.Application.Subscribeds.Result;
using RocketMqtt.Util.Model;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 订阅
/// </summary>
public class SubscribedController : BaseController
{
    private readonly ISubscribedQuery _subscribedQuery;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="subscribedQuery"></param>
    public SubscribedController(ISubscribedQuery subscribedQuery)
    {
        _subscribedQuery = subscribedQuery;
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<PageListResult<SubscribedResult>> PageListAsync(SubscribedPageRequest request)
    {
        return _subscribedQuery.GetPageListAsync(request);
    }
}