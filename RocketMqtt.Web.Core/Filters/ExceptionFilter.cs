using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Web.Core.Filters;

public class ExceptionFilter: IAsyncExceptionFilter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        //判断异常是否已经处理
        if (!context.ExceptionHandled)
        {
            var result = new ApiResult(StatusCodes.Status500InternalServerError, false, context.Exception);

            context.Result = new JsonResult(result);

            context.ExceptionHandled = true;
        }
    }
}