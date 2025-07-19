using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace ApiClientes.Extesion;

public static class ConfigureExceptionGlobalExtesion
{
    public static void UseExceptionGlobalHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
            appError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Applicaton/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Menssage = contextFeature.Error.Message,
                        StatusCode = context.Response.StatusCode,
                        Trace = contextFeature.Error.StackTrace
                    }));
                }
            })
        );
    }
}