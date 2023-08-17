using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RocketMqtt.Application.Users;
using RocketMqtt.Application.Users.Command;
using RocketMqtt.Application.Users.Request;
using RocketMqtt.Application.Users.Result;
using RocketMqtt.Util.Model;
using RocketMqtt.Web.Core.Jwt;

namespace RocketMqtt.Web.Core.Controllers;

/// <summary>
/// 用户
/// </summary>
public class UserController : BaseController
{
    private readonly TokenService _tokenService;
    private readonly IMediator _mediator;
    private readonly IUserQuery _userQuery;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="tokenService"></param>
    /// <param name="mediator"></param>
    /// <param name="userQuery"></param>
    public UserController(TokenService tokenService, IMediator mediator, IUserQuery userQuery)
    {
        _tokenService = tokenService;
        _mediator = mediator;
        _userQuery = userQuery;
    }

    /// <summary>
    /// 登录
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

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> AddAsync(CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    /// <summary>
    /// 分页列表
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public Task<PageListResult<UserResult>> PageListAsync(UserPageRequest request)
    {
        return _userQuery.GetPageListAsync(request);
    }
}