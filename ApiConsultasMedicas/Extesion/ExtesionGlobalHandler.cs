using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Diagnostics;

namespace ApiConsultasMedicas.Extesion;

public static class ExtesionGlobalHandler
{
    public static void UseExceptioNGlobalHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(AppError =>
        {
            AppError.Run(async context =>
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "Application/Json";
                var error = context.Features.Get<IExceptionHandlerFeature>();
                if (error is not null)
                {
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        Menssage = error.Error.Message,
                        Trace = error.Error.StackTrace,
                        StatusCode = 500
                    }));
                }
            });
        });
    }
}