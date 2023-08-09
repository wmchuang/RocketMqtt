using MQTTnet.AspNetCore;
using NLog.Web;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Web.Core;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.UseKestrel(o =>
{
    // This will allow MQTT connections based on TCP port 1883.
    o.ListenAnyIP(1884, l => l.UseMqtt());
    o.ListenAnyIP(8080); 
});

builder.WebHost.UseNLog();

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);


app.MigrateDbContext();
app.Run();