using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Serilog;
using Serilog.Events;

namespace DataAnalyzer.Extensions
{
    public class LoggingMiddlewareAnalyzer
    {
        #region Fields

        private readonly RequestDelegate next;

        #endregion

        #region Constructors and Destructors

        public LoggingMiddlewareAnalyzer(RequestDelegate next)
        {
            this.next = next ?? throw new ArgumentNullException(nameof(next));
        }

        #endregion

        #region Public Methods and Operators

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var sw = Stopwatch.StartNew();
            var logger = Log.ForContext<LoggingMiddlewareAnalyzer>();

            try
            {
                logger.Write(LogEventLevel.Information, "Starting HTTP {RequestMethod} {RequestPath}", httpContext.Request.Method, httpContext.Request.Path);

                await this.next(httpContext);
                sw.Stop();

                var statusCode = httpContext.Response?.StatusCode;

                logger.Write(LogEventLevel.Information, "Finished HTTP {RequestMethod} {RequestPath} with status {StatusCode} in {Elapsed:0.0000} ms.", httpContext.Request.Method, httpContext.Request.Path, statusCode, sw.Elapsed.TotalMilliseconds);
            }
            catch (Exception ex)
            {
                sw.Stop();
                logger.Error(ex, "Finished HTTP {RequestMethod} {RequestPath} with status {StatusCode} in {Elapsed:0.0000} ms.", httpContext.Request.Method, httpContext.Request.Path, 500, sw.Elapsed.TotalMilliseconds);

                if (httpContext.Response != null && !httpContext.Response.HasStarted)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    httpContext.Response.ContentType = "application/json";

                    await httpContext.Response.WriteAsync($"{{\"errors\": [\r\n{{\r\n\"code\": 9999,\r\n\"description\":\"Something went wrong.\"\r\n}}\r\n]}}").ConfigureAwait(false);
                }
            }
        }

        #endregion
    }
}