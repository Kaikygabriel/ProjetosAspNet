using BlibiotecaApi.Model;
using Microsoft.AspNetCore.Diagnostics;

namespace BlibiotecaApi.Extesion;

public static class ConfigureExceptionsHandler
{
    public static void UseConfigureExceptionsGlobal(this IApplicationBuilder app)
    {
        app.UseExceptionHandler( appErro =>
        {
            appErro.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Menssage = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace
                    }.ToString());
                }
            });
        });
    }
}