using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace BlibiotecaApi.ExtesionMethods;

public static  class ConfigureExceptionGlobal
{
    public static void UseConfigureExceptionGlobalHandler(this IApplicationBuilder app)
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
                        StatusCode = context.Response.StatusCode,
                        Menssage = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace
                    }));
                }
            });
        });
    }
}