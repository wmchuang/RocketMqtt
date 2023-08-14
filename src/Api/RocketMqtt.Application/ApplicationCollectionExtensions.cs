using RocketMqtt.Application.ConnInfos;
using RocketMqtt.Application.Mapper;
using RocketMqtt.Application.Subscribeds;
using RocketMqtt.Application.Topics;

namespace RocketMqtt.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMapste();
        
        services.AddTransient<IConnInfoQuery, ConnInfoQuery>();
        services.AddTransient<ITopicQuery, TopicQuery>();
        services.AddTransient<ISubscribedQuery, SubscribedQuery>();
    }
}