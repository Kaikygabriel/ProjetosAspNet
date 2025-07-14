using Microsoft.AspNetCore.Mvc.Filters;

namespace BlibiotecaApi.Filters;

public class LoggingMenssageFilter(ILogger<LoggingMenssageFilter> logger) : IActionFilter
{
    private readonly ILogger<LoggingMenssageFilter> _logger = logger;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation( $"Inicio");
        _logger.LogInformation( $"{context.Controller.ToString()}");
        _logger.LogInformation( $"Validação model state{context.ModelState.IsValid}" );
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation( $"##########################");
        _logger.LogInformation( $"Fim");
    }
}
