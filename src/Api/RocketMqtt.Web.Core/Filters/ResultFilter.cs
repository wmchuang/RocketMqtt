using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Web.Core.Filters;

/// <summary>
/// 结果过滤器
/// </summary>
public class ResultFilter : IAsyncResultFilter, IAsyncExceptionFilter
{
    /// <summary>
    /// 对返回结果进行发封装
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        Console.WriteLine("OnResultExecutionAsync Start");
        var data = GetResult(context.Result);

        if (data is ApiResult)
        {
            context.Result = new OkObjectResult(data);
        }
        else
        {
            var result = new ApiResult<object?>(StatusCodes.Status200OK, true, data);
            context.Result = new OkObjectResult(result);
        }

        await next();

        Console.WriteLine("OnResultExecutionAsync End");

        object? GetResult(IActionResult res)
        {
            return res switch
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

    /// <summary>
    /// 发生错误时
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public Task OnExceptionAsync(ExceptionContext context)
    {
        //判断异常是否已经处理
        if (!context.ExceptionHandled)
        {
            var result = new ApiResult(StatusCodes.Status500InternalServerError, false, context.Exception.Message);

            context.Result = new JsonResult(result);

            context.ExceptionHandled = true;
        }

        return Task.CompletedTask;
    }
}