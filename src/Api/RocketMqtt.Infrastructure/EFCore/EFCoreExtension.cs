using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RocketMqtt.Infrastructure.EFCore;

public static class EFCoreExtension
{
    public static void AddEFCoreContext(this IServiceCollection services, IConfiguration configuration,string migrationsAssembly)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite(configuration["ConnectionString"],
                sqliteOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                });
        });
    }
}