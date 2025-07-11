using System.Net;
using CatalogoApi.Model;
using Microsoft.AspNetCore.Diagnostics;

namespace CatalogoApi.Extesions
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHanler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFaeture = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFaeture != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            Menssage = contextFaeture.Error.Message,
                            StatusCode = context.Response.StatusCode,
                            Trace = contextFaeture.Error.StackTrace
                        }.ToString());
                    }
                });
            });
        }
    }
}
