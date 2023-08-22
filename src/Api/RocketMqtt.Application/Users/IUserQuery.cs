using RocketMqtt.Application.Users.Request;
using RocketMqtt.Application.Users.Result;
using RocketMqtt.Util.Model;

namespace RocketMqtt.Application.Users;

public interface IUserQuery
{
    Task<PageListResult<UserResult>> GetPageListAsync(UserPageRequest request);
    Task<LoginResult> LoginAsync(LoginRequest request);
}