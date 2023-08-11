using System.Reflection;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace RocketMqtt.Application.Mapper;

public static class MapsteExtension
{
    public static IServiceCollection AddMapste(this IServiceCollection services, params Assembly[] assemblies)
    {
        // 获取全局映射配置
        var config = TypeAdapterConfig.GlobalSettings;

        // 扫描所有继承  IRegister 接口的对象映射配置
        config.Scan(Assembly.GetExecutingAssembly());

        return services;
    }
}