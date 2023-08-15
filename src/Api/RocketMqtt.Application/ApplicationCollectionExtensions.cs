using System.Reflection;
using RocketMqtt.Application.Mapper;

namespace RocketMqtt.Application;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMapste();

        // 获取当前程序集
        var assembly = Assembly.GetExecutingAssembly();

        // 获取结尾是Query的类型
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Query")).ToList();

        // 批量注入
        foreach (var type in serviceTypes)
        {
            var interfaceList = type.GetInterfaces();
            if (!interfaceList.Any()) continue;
            var inter = interfaceList.First();
            services.AddTransient(inter, type);
        }
    }
}