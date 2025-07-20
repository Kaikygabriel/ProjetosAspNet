using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace ApiClientes.Extesion;

public static class ConfigureExceptionGlobalExtesion
{
    public static void UseExceptionGlobalHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError=>
            appError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/json";
                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error != null)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize((new
                    {
                        StatusCode= context.Response.StatusCode,
                        Trace=error.Error.StackTrace,
                        Menssage=error.Error.Message
                    })));
                }
            })
            );
    }
}