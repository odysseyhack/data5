using System.Reflection;

using Microsoft.AspNetCore.Hosting;
using Serilog;

namespace DotNetCore.Extensions
{
    public static class WebHostBuilderExtensionsAnalyzer
    {
        #region Public Methods and Operators

        public static IWebHostBuilder UseLogging(this IWebHostBuilder builder, string path = null)
        {
            return builder.ConfigureLogging(
                (hostingContext, logging) =>
                    {
                        if (path == null)
                        {
                            path = $"D:/log/{Assembly.GetEntryAssembly().GetName().Name}/log.txt";
                        }

                        Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.File(
                            path,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                            rollOnFileSizeLimit: true,
                            fileSizeLimitBytes: 10000000,
                            rollingInterval: RollingInterval.Day).CreateLogger();

                        logging.AddSerilog(dispose: true);
                    });
        }

        #endregion
    }
}