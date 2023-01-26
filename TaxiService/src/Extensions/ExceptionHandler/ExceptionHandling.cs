using System.Net;
using Entities.General;
using Microsoft.AspNetCore.Diagnostics;
using TaxiService.Extensions.ExceptionHandler.Interfaces;

namespace TaxiService.Extensions.ExceptionHandler;

public static class ExceptionHandling
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError($"Something went wrong: {contextFeature.Error}");
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = "Internal Server Error. Please try later"
                    }.ToString());
                }
            });
        });
    }
}