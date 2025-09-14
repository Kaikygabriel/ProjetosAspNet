using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace NotifiMe.Extesion;

public static class MiddlewareGlobalException
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async appContext =>
            {
                appContext.Response.StatusCode = 500;
                appContext.Response.ContentType = "Appplication/Json";
                var featureError = appContext.Features.Get<IExceptionHandlerFeature>();
                if (featureError is not null)
                    await appContext.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Menssage = featureError.Error.Message,
                        StatusCode = 500,
                        Trace = featureError.Error.StackTrace
                    }));
            });
        }); 
    }   
}