using RocketMqtt.Application.Common;
using RocketMqtt.Application.Users.Request;
using RocketMqtt.Application.Users.Result;
using RocketMqtt.Domain.Domain;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Util.Model;
using SqlSugar;

namespace RocketMqtt.Application.Users;

public class UserQuery : IUserQuery
{
    private readonly SqlSugarScopeProvider _baseDbClient;

    public UserQuery(BaseDbClient baseDbClient)
    {
        _baseDbClient = baseDbClient.Db;
    }

    public async Task<PageListResult<UserResult>> GetPageListAsync(UserPageRequest request)
    {
        return await _baseDbClient.Queryable<User>()
            .WhereIF(!string.IsNullOrWhiteSpace(request.UserName), x => x.UserName.Contains(request.UserName))
            .ToPagedListAsync<User, UserResult>(request.PageIndex, request.PageSize);
    }
}