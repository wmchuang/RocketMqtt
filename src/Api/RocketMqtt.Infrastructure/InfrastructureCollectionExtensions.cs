using Microsoft.Extensions.DependencyInjection;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Infrastructure.DbContext;
using RocketMqtt.Infrastructure.Repository;
using RocketMqtt.Infrastructure.SqlSugar;

namespace RocketMqtt.Infrastructure;

public static class InfrastructureCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        // services.AddTransient(typeof(IDomainRepository<>), typeof(DomainRepository<>));
        services.AddTransient(typeof(IRepository<>), typeof(SqlSugarRepository<>));
    }
}