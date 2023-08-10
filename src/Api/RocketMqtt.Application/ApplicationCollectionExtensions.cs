using RocketMqtt.Application.ConnInfos;

namespace RocketMqtt.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IConnInfoQuery, ConnInfoQuery>();
    }
}