using Microsoft.AspNetCore.Mvc;
using RocketMqtt.Application.User.Request;
using RocketMqtt.Application.User.Result;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 用户
/// </summary>
public class UserController : BaseController
{
    /// <summary>
    /// Login
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public LoginResult Login(LoginRequest request)
    {
        return new LoginResult()
        {
            Token = "123",
            FullName = "Admin"
        };
    }
}