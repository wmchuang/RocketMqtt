using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RocketMqtt.Domain.Domain;

namespace RocketMqtt.Infrastructure.SqlSugar;

public static class IHostExtension
{
    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <param name="webHost"></param>
    public static void MigrateDbContext(this IHost webHost)
    {
        using (var scope = webHost.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<BaseDbClient>();

            var types = typeof(EntityBase).Assembly.GetTypes()
                .Where(type => type.IsClass 
                               && !type.IsAbstract 
                               && typeof(EntityBase).IsAssignableFrom(type))
                .ToArray();

            context.Db.CodeFirst.SetStringDefaultLength(200).InitTables(types); //根据types创建表
        }
    }
}