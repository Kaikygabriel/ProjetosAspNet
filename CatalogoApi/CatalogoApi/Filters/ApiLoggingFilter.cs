using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CatalogoApi.Filters
{
    public class ApiLoggingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggingFilter> _logger;

        public ApiLoggingFilter(ILogger<ApiLoggingFilter>  logger)
        {
            _logger = logger;
        }

        //antes
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Executando em OnActionExcuted ");
            _logger.LogInformation($"Model state : {context.ModelState.IsValid}");
            _logger.LogInformation("#################################");
        }
        //depois
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("#################################");
            _logger.LogInformation($"Model state : {context.ModelState.IsValid}");
            _logger.LogInformation("Executando depois do action");
        }
    }
}
