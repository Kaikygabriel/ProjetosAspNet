using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiClientes.Filters;

public class ExceptionFilterGlobal : IExceptionFilter
{
    private readonly ILogger<ExceptionFilterGlobal> _logger;

    public ExceptionFilterGlobal(ILogger<ExceptionFilterGlobal> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception.Message);
        context.Result = new ObjectResult("Error 500.")
        {
            StatusCode = 500
        };
    }
}