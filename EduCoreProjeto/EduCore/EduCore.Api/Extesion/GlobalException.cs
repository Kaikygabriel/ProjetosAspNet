using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace EduCore.Api.Extesion;

public static class GlobalException
{
    public static void UseExceptionGlobal(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "Application/Json";
                context.Response.StatusCode = 500;
                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error is not null)
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Stack = error.Error.StackTrace,
                        Menssage = error.Error.Message
                    }));
            });
        });
    }
}