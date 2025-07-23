using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APiCursos.Filter;

public class ExceptionGlobalFilter : IExceptionFilter
{
    public ExceptionGlobalFilter(ILogger<ExceptionGlobalFilter> logger)
    {
        _logger = logger;
    }
    private readonly ILogger<ExceptionGlobalFilter> _logger;

    public void OnException(ExceptionContext context)
    {
        _logger.LogError("Error 500.");
        context.Result = new ObjectResult("Error internal : 500")
        {
            StatusCode = 500
        };
    }
}
 
