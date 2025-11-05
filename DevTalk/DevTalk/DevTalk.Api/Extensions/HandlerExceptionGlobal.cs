using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace DevTalk.Api.Extensions;

public static class HandlerExceptionGlobal
{
    public static void UseHandlerExceptionGlobal(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
            x.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/Json";
                var menssageError = context.Features.Get<IExceptionHandlerFeature>();
                if (menssageError is not null)
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        context.Response.StatusCode,
                        menssageError.Error.Message,
                        menssageError.Error.StackTrace,
                    }));
            }));
    }
}