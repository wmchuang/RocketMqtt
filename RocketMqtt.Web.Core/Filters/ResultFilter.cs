using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Web.Core.Filters;

/// <summary>
/// 操作过滤器
/// </summary>
public class ResultFilter : IAsyncResultFilter
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        var data = GetResult(context.Result);

        var result = new ApiResult<object?>(StatusCodes.Status200OK, true, data);
        context.Result = new OkObjectResult(result);

        await next();
    }

    private object? GetResult(IActionResult result)
    {
        return result switch
        {
            // 处理内容结果
            ContentResult content => content.Content,
            // 处理对象结果
            ObjectResult obj => obj.Value,
            // 处理 JSON 对象
            JsonResult json => json.Value,
            _ => null,
        };
    }
}