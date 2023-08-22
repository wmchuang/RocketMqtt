using RocketMqtt.Application.Common;
using RocketMqtt.Application.Users.Request;
using RocketMqtt.Application.Users.Result;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Util.Exception;
using RocketMqtt.Util.Model;
using RocketMqtt.Util.Security;
using SqlSugar;

namespace RocketMqtt.Application.Users;

public class UserQuery : IUserQuery
{
    private readonly SqlSugarScopeProvider _baseDbClient;

    public UserQuery(BaseDbClient baseDbClient)
    {
        _baseDbClient = baseDbClient.Db;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request)
    {
        var user = await _baseDbClient.Queryable<User>().FirstAsync(x => x.UserName == request.UserName);
        if (user == null)
            throw new OperationException("用户名或密码错误!");

        string password = string.Concat(request.Password, user.Salt ?? "");
        if (!user.Password.Equals(password.ToMd5String()))
            throw new OperationException("用户名或密码错误!");

        return new LoginResult()
        {
            UserId = user.Id,
            UserName = user.UserName,
        };
    }

    public async Task<PageListResult<UserResult>> GetPageListAsync(UserPageRequest request)
    {
        return await _baseDbClient.Queryable<User>()
            .WhereIF(!string.IsNullOrWhiteSpace(request.UserName), x => x.UserName.Contains(request.UserName))
            .ToPagedListAsync<User, UserResult>(request.PageIndex, request.PageSize);
    }
}