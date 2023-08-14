using MQTTnet.AspNetCore;
using NLog.Web;
using RocketMqtt.Infrastructure.SqlSugar;
using RocketMqtt.Web.Core;

var builder = WebApplication.CreateBuilder(args);

var kestrelSettings = builder.Configuration.GetSection("KestrelSettings");
var mqttPipeline = kestrelSettings.GetValue<int>("MQTTPipeline");
var httpPipeline = kestrelSettings.GetValue<int>("HttpPipeline");
var httpsPipeline = kestrelSettings.GetValue<int>("HttpsPipeline");

builder.WebHost.UseKestrel(o =>
{
    // This will allow MQTT connections based on TCP port 1883.
    o.ListenAnyIP(mqttPipeline, l => l.UseMqtt());
    o.ListenAnyIP(httpPipeline); 
    o.ListenAnyIP(httpsPipeline, l => l.UseHttps());
});

builder.WebHost.UseNLog();

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);


app.MigrateDbContext();
app.Run();