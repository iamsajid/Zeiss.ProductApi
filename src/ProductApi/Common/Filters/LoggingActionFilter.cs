namespace ProductApi.Common.Filters;

using Microsoft.AspNetCore.Mvc.Filters;

public class LoggingActionFilter : IActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller.GetType().Name;
            var action = context.ActionDescriptor.DisplayName;
            var route = context.HttpContext.Request.Path;
            var method = context.HttpContext.Request.Method;

            _logger.LogInformation($"Executing {method} {route} on {controller}.{action}");

            // Log parameters
            foreach (var param in context.ActionArguments)
            {
                _logger.LogInformation($"Parameter {param.Key}: {param.Value}");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller.GetType().Name;
            var action = context.ActionDescriptor.DisplayName;
            var responseType = context.Result?.GetType().Name ?? "No response";

            if (context.Exception == null)
            {
                _logger.LogInformation($"Completed {controller}.{action}. Response type: {responseType}");
            }
            else
            {
                _logger.LogError(context.Exception, $"Exception in {controller}.{action}. Response type: {responseType}");
            }
        }
}
