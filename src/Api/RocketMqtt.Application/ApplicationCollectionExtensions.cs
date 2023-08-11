using RocketMqtt.Application.ConnInfos;
using RocketMqtt.Application.Mapper;

namespace RocketMqtt.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMapste();
        
        services.AddTransient<IConnInfoQuery, ConnInfoQuery>();
    }
}