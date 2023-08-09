using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketMqtt.Application.User.Request;
using RocketMqtt.Application.User.Result;
using RocketMqtt.Web.Core.Jwt;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 用户
/// </summary>
public class UserController : BaseController
{
    private readonly TokenService _tokenService;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tokenService"></param>
    public UserController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    /// <summary>
    /// Login
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<LoginResult> Login(LoginRequest request)
    {
        var token = await _tokenService.CreateTokenAsync("1231231312");
        return new LoginResult()
        {
            Token = token,
            FullName = "Admin"
        };
    }
}