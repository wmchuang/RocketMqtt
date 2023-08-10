using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RocketMqtt.Web.Core.Results;

namespace RocketMqtt.Web.Core.Filters;

/// <summary>
/// 模型验证
/// </summary>
public class DataValidationFilter : IAsyncActionFilter
{
    /// <summary>
    /// 执行动作方法前后进行拦截和处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {

            var errors = context.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
            var result = new ApiResult(StatusCodes.Status400BadRequest, false, errors);

            context.Result = new JsonResult(result);
            
            return;
        }

        
        await next();

    }
}