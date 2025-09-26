using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Filmes.CrossCuting.Extesions;

public static class ExtesionExceptionGlobalHandler
{
    public static void UseExceptionGlobalHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
            appError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/Json";
                var ErrorFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (ErrorFeature is not null)
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        StatusCode = context.Response.StatusCode,
                        StackTrace = ErrorFeature.Error.StackTrace,
                        Menssage = ErrorFeature.Error.Message
                    }));
            }));
    }
}