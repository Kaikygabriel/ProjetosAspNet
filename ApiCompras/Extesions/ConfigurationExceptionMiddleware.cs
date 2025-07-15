using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace ApiCompras.Extesions;

public static class ConfigurationExceptionMiddleware
{
    public static void UseExceptionGlobalHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Menssage = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace,
                        StatusCodoe = context.Response.StatusCode
                    }));
                }
            });
        });
    } 
}