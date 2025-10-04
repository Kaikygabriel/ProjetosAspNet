using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Biblioteca.Api.Filters;

public class ExceptionFilters:IExceptionFilter 
{
    public void OnException(ExceptionContext context)
    {
        var response = new
        {
            Message = "Ocorreu um erro inesperado no servidor.",
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true; 
    }
}