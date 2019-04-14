using System.IO;
using System.Net;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DotNetCore.Extensions
{
    public static class StartupExtensions
    {
        #region Public Methods and Operators

        public static IMvcBuilder AddDefaultJsonSettings(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddJsonOptions(
                opts =>
                    {
                        opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        opts.SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                        opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    });
        }

        public static IServiceCollection AddGzipCompression(this IServiceCollection services)
        {
            return services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });
        }

        public static IServiceCollection AddLowerCaseUrls(this IServiceCollection services)
        {
            return services.AddRouting(options => { options.LowercaseUrls = true; });
        }

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            return services.AddApiVersioning(o => o.ReportApiVersions = true);
        }

        public static IApplicationBuilder UseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }

        public static IApplicationBuilder UseHealthPage(this IApplicationBuilder app)
        {
            return app.Map(
                "/health",
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.OK;
                            context.Response.ContentType = "application/json";
                            await context.Response.WriteAsync($"{{\"health\": \"OK\"}}").ConfigureAwait(false);
                        });
                });
        }

        #endregion
    }
}