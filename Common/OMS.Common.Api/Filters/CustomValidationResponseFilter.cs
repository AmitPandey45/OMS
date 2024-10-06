using Microsoft.AspNetCore.Mvc.Filters;
using OMS.Common.Api.Helpers;
using OMS.Common.Api.Middlewares;

namespace OMS.Common.Api.Filters
{
    public class CustomValidationResponseFilter : IActionFilter
    {
        private readonly ILogger<CustomValidationMiddleware> _logger;

        public CustomValidationResponseFilter(ILogger<CustomValidationMiddleware> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Check if the ModelState is valid
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

                _logger.LogWarning("Validation errors occurred: {@Errors}", errors);

                // Extracting controller and action names
                var controllerName = context.ActionDescriptor.RouteValues["controller"];
                var actionName = context.ActionDescriptor.RouteValues["action"];
                var methodName = $"{controllerName}.{actionName}";

                var badRequestApiResponse = ResponseHelper.CreateValidationResponse(methodName, errors);
                context.Result = badRequestApiResponse;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
