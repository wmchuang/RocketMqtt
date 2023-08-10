using RocketMqtt.Infrastructure.SqlSugar;
using SqlSugar;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqlSugarExtensions
{
    public static void AddSqlSugarSetup(this IServiceCollection services, List<ConnectionConfig> connectionConfigs)
    {
        if (connectionConfigs == null)
            throw new ArgumentException("数据库链接配置错误");

        // 注册 SqlSugar 客户端
        services.AddScoped<ISqlSugarClient>(u =>
        {
            var db = new SqlSugarClient(connectionConfigs);
            //单例参数配置，所有上下文生效
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //执行时间超过1秒
                if (db.Ado.SqlExecutionTime.TotalSeconds > 1)
                {
                    //代码CS文件名
                    var fileName = db.Ado.SqlStackTrace.FirstFileName;
                    //代码行数
                    var fileLine = db.Ado.SqlStackTrace.FirstLine;
                    //方法名
                    var FirstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
                    Console.WriteLine($"执行时间超过1秒，建议优化，fileName：{fileName}，fileLine：{fileLine}，FirstMethodName：{FirstMethodName}，sql：{sql}");
                }

                Console.WriteLine($"SQL:{sql}，pars：{pars}");
            };
            return db;
        });

        services.AddScoped<BaseDbClient>();
    }
}