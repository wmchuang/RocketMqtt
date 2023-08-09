using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 基本控制器
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class BaseController : ControllerBase
{
    
}