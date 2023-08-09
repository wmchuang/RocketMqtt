using SqlSugar;

namespace RocketMqtt.Infrastructure.SqlSugar;

public class BaseDbClient
{
    public readonly SqlSugarScopeProvider Db;

    public BaseDbClient(ISqlSugarClient db)
    {
        Db = db.AsTenant().GetConnectionScope("RocketMqtt");
    }
}