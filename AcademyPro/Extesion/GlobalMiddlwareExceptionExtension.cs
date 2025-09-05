using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace AcademyPro.Extesion;

public static class GlobalMiddlwareExceptionExtension
{
    public static void UseExceptionGlobal(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
            appError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/Json";
                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error is not null)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Menssage = error.Error.Message,
                        stack = error.Error.StackTrace,
                        StatusCode = context.Response.StatusCode
                    }));
                }
            }));
    }
}