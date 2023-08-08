using Microsoft.Extensions.DependencyInjection;
using RocketMqtt.Domain.Repository;
using RocketMqtt.Infrastructure.DbContext;
using RocketMqtt.Infrastructure.Repository;

namespace RocketMqtt.Infrastructure;

public static class InfrastructureCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient(typeof(IDomainRepository<>), typeof(DomainRepository<>));
    }
}