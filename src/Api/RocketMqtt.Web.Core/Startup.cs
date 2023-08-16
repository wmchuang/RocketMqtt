using System.Reflection;
using Coldairarrow.Util;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MQTTnet.AspNetCore;
using RocketMqtt.Application;
using RocketMqtt.Infrastructure;
using RocketMqtt.Infrastructure.EFCore;
using RocketMqtt.Web.Core.Controllers;
using RocketMqtt.Web.Core.Filters;

namespace RocketMqtt.Web.Core;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging();
       
        
        services.AddHostedMqttServer(
            optionsBuilder => { optionsBuilder.WithDefaultEndpoint(); });

        services.AddMqttConnectionHandler();
        services.AddConnections();
        services.AddSwaggerConfig(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "RockerMqtt Api",
                Version = "v1.0"
            });
        });
        
        services.AddJwt();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });


        services.AddSingleton<MqttController>();

        new IdHelperBootstrapper().SetWorkderId(0).Boot();

        services.AddInfrastructure();
        services.AddApplication();
        
        var connectionConfigs = Configuration.GetSection("ConnectionConfigs").Get<List<SqlSugar.ConnectionConfig>>();
        services.AddSqlSugarSetup(connectionConfigs);

        var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        // services.AddEFCoreContext(Configuration,migrationsAssembly);

        services.AddMediatR(typeof(ApplicationCollectionExtensions));

        var m = Configuration["ConnectionString"];
        Console.WriteLine(m);
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite( m,
                sqliteOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(migrationsAssembly);
                });
        });
        
        
        services.AddControllers(options =>
        {
            options.Filters.Add<DataValidationFilter>();
            options.Filters.Add<ResultFilter>();
        }).ConfigureApiBehaviorOptions(options =>
        {
            //关掉自带的模型验证
            options.SuppressModelStateInvalidFilter = true;
        });
        
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseRouting();

        app.UseCors("AllowAll");
        
        app.UseAuthentication();
        app.UseAuthorization();
        
        var mqttController = app.ApplicationServices.GetService<MqttController>();
        app.UseMqttServer(
            server =>
            {
                // 常用方法 https://github.com/dotnet/MQTTnet/wiki/Server-and-client-method-documentation#methods
                /*
                 * ApplicationMessageNotConsumedAsync - 应用消息未被消费
                   ClientAcknowledgedPublishPacketAsync - 客户端确认发布数据包
                   ClientConnectedAsync - 客户端已连接
                   ClientDisconnectedAsync - 客户端已断开连接
                   ClientSubscribedTopicAsync - 客户端订阅主题
                   ClientUnsubscribedTopicAsync - 客户端取消订阅主题
                   InterceptingClientEnqueueAsync - 拦截客户端入队操作
                   InterceptingInboundPacketAsync - 拦截传入数据包
                   InterceptingOutboundPacketAsync - 拦截传出数据包
                   InterceptingPublishAsync - 拦截发布操作
                   InterceptingSubscriptionAsync - 拦截订阅操作
                   InterceptingUnsubscriptionAsync - 拦截取消订阅操作
                   LoadingRetainedMessageAsync - 加载保留消息
                   PreparingSessionAsync - 准备会话
                   RetainedMessageChangedAsync - 保留消息已更改
                   RetainedMessagesClearedAsync - 清除保留消息
                   SessionDeletedAsync - 会话已删除
                   StartedAsync - 已启动
                   StoppedAsync - 已停止
                   ValidatingConnectionAsync - 验证连接
                 */
                server.ValidatingConnectionAsync += mqttController.ValidateConnection;
                server.ClientConnectedAsync += mqttController.OnClientConnected;
                server.ClientDisconnectedAsync += mqttController.ClientDisconnectedAsync;
                server.ClientSubscribedTopicAsync += mqttController.ClientSubscribedTopicAsync;
                server.ClientUnsubscribedTopicAsync += mqttController.ClientUnsubscribedTopicAsync;
            });

        app.UserSwaggerUi();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}