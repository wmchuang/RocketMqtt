using System.Xml.Linq;
using System.Xml.XPath;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerGen;
using IGeekFan.AspNetCore.Knife4jUI;
using Microsoft.OpenApi.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class SwaggerServiceCollectionExtensions
{
    public static string DocName = "RocketMqtt";
    public static void AddSwaggerConfig(this IServiceCollection services,
        Action<SwaggerGenOptions> swaggerOptions)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(DocName, new OpenApiInfo
            {
                Title = "RocketMqtt",
                Version = "v1.0"
            });
            swaggerOptions?.Invoke(options);

            var directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var fileInfos = directoryInfo.GetFiles();
            var xmlFiles = fileInfos.Where(x => x.Name.EndsWith(".xml"));
            foreach (var file in xmlFiles)
            {
                var xmlDoc = XDocument.Load(file.FullName);

                options.IncludeXmlComments(() => new XPathDocument(xmlDoc.CreateReader()), true);
            }
        });
    }

    /// <summary>
    /// 使用swagger
    /// </summary>
    /// <param name="app">IApplicationBuilder</param>
    public static void UserSwaggerUi(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseKnife4UI(c =>
        {
            c.SwaggerEndpoint($"/swagger/{DocName}/swagger.json", "Api");
            c.RoutePrefix = "";
            c.DocExpansion(DocExpansion.None);
            c.DocumentTitle = "Api";
            c.EnableFilter();
        });
    }
}