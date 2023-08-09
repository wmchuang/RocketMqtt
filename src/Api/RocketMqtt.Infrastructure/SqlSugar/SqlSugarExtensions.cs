using RocketMqtt.Infrastructure.SqlSugar;
using SqlSugar;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqlSugarExtensions
{
    public static void AddSqlSugarSetup(this IServiceCollection services,List<ConnectionConfig> connectionConfigs)
    {
        if (connectionConfigs == null)
            throw new ArgumentException("数据库链接配置错误");
            
        // 注册 SqlSugar 客户端
        services.AddScoped<ISqlSugarClient>(u =>
        {
            var sqlSugarClient = new SqlSugarClient(connectionConfigs);

            return sqlSugarClient;
        });

        services.AddScoped<BaseDbClient>();
    }
    
    
}