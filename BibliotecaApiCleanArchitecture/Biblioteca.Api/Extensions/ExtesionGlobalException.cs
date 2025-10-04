using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace Biblioteca.Api.Extensions;

public static class ExtesionGlobalException
{
    public static void UseGlobalException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(Erroapp =>
            Erroapp.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/json";
                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error is not null)
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        statusCode = 500,
                        Menssage = error.Error.Message,
                        StackTrace = error.Error.StackTrace
                    }));
            }));
    }
}