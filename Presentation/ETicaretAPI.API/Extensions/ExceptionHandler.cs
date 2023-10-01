using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mime;

namespace ETicaretAPI.API.Extensions
{
    static public class ExceptionHandler
    {

        public static void ExceptionHandlerExtension<T>(this WebApplication application,ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (context != null)
                    {
                        logger.LogError(contextFeature.Error.Message);
                        await context.Response.WriteAsJsonAsync(new
                        {
                            StatusCode = (int)context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Title = "Hata"
                        });
                    }
                });
            });

        }
    }
}
