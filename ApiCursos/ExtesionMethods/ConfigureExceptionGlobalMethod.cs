using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace ApiCursos.ExtesionMethods;

public static class ConfigureExceptionGlobalMethods
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
                    await context.Response.WriteAsync(JsonSerializer.Serialize(
                        new
                        {
                            StatusCode = context.Response.StatusCode,
                            Menssage = contextFeature.Error.Message,
                            Trace = contextFeature.Error.StackTrace
                        }
                    ));
                }
            });
        });
    }
}