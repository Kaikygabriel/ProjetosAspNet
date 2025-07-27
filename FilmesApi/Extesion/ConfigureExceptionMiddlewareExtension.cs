using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace FilmesApi.Extesion;

public static class ConfigureExceptionMiddlewareExtension
{
    public static void UseExceptionGlobalHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
            appError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/Json";
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (errorFeature is not null)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Menssage = errorFeature.Error.Message,
                        Trace = errorFeature.Error.StackTrace,
                        StatusCode = context.Response.StatusCode
                    }));
                }
            }));
    }
}