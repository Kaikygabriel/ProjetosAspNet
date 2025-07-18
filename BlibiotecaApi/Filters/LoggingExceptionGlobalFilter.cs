using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlibiotecaApi.Filters;

public class LoggingExceptionGlobalFilter  : IExceptionFilter
{
    private readonly ILogger<LoggingExceptionGlobalFilter> _logger;

    public LoggingExceptionGlobalFilter(ILogger<LoggingExceptionGlobalFilter>logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception.Message, "Erro 500");
        context.Result = new ObjectResult("Erro 500")
        {
            StatusCode = 500
        };
    }
}