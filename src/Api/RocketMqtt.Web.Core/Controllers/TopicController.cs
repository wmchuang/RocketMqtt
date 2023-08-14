using Microsoft.AspNetCore.Mvc;
using RocketMqtt.Application.Common;
using RocketMqtt.Application.Topics;
using RocketMqtt.Application.Topics.Request;
using RocketMqtt.Application.Topics.Result;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 主题
/// </summary>
public class TopicController : BaseController
{
    private readonly ITopicQuery _topicQuery;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="topicQuery"></param>
    public TopicController(ITopicQuery topicQuery)
    {
        _topicQuery = topicQuery;
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<PageListResult<TopicResult>> PageListAsync(TopicPageRequest request)
    {
        return _topicQuery.GetPageListAsync(request);
    }
}