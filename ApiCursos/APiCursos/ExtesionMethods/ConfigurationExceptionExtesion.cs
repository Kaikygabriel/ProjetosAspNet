using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace ApiCursos.ExtesionMethods;

public static class ConfigurationExceptionExtesion
{
    public static void UseExceptionGlobalHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appErro =>
            appErro.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is not null)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Menssage = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace,
                        StatusCode = context.Response.StatusCode
                    }));
                }
            }));
    }
}