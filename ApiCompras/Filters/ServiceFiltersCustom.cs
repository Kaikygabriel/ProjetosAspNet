using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCompras.Filters;

public class ServiceFiltersCustom  : IActionFilter
{
    private readonly ILogger<ServiceFiltersCustom> _logger;

    public ServiceFiltersCustom(ILogger<ServiceFiltersCustom> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("##########");
        _logger.LogInformation("Antes da action");
        _logger.LogInformation($"Model state : {context.ModelState.IsValid}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("##########");
        _logger.LogInformation("Depois da action");
        _logger.LogInformation($" Controller : {context.Controller}");
    }
}