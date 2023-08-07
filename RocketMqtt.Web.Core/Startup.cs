using Coldairarrow.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MQTTnet.AspNetCore;
using RocketMqtt.Application;
using RocketMqtt.Infrastructure;
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
        services.AddControllers(options =>
            {
                options.Filters.Add<ExceptionFilter>();
                options.Filters.Add<ResultFilter>();
            });
        services.AddHostedMqttServer(
            optionsBuilder =>
            {
                optionsBuilder.WithDefaultEndpoint();
            });

        services.AddMqttConnectionHandler();
        services.AddConnections();
        services.AddSwaggerConfig(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "WeChat.ALL.MP.API",
                Version = "v1.0"
            });
        });

        services.AddSingleton<MqttController>();

        new IdHelperBootstrapper().SetWorkderId(0).Boot();

        services.AddInfrastructure();
        services.AddApplication();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseRouting();

        app.UseCors();
        var mqttController = app.ApplicationServices.GetService<MqttController>();
        app.UseMqttServer(
            server =>
            {
                /*
                 * Attach event handlers etc. if required.
                 */

                server.ValidatingConnectionAsync += mqttController.ValidateConnection;
                server.ClientConnectedAsync += mqttController.OnClientConnected;
                server.ClientDisconnectedAsync += mqttController.ClientDisconnectedAsync;
            });

        app.UserSwaggerUi();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}