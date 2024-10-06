using Microsoft.AspNetCore.Mvc;
using OMS.Common.Api.Helpers;
using System.Net;

namespace OMS.Common.Api.ResponseFactories
{
    public class ModelStateResponseFactory
    {
        public static IActionResult CreateResponse(ActionContext context, ILogger<ModelStateResponseFactory> logger)
        {
            var modelState = context.ModelState;
            var errors = modelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            // Extracting controller and action names
            var controllerName = context.ActionDescriptor.RouteValues["controller"];
            var actionName = context.ActionDescriptor.RouteValues["action"];
            var methodName = $"{controllerName}.{actionName}";

            logger.LogWarning("Validation errors occurred: {@Errors}", errors);
            var badRequestApiResponse = ResponseHelper.CreateValidationResponse(methodName, errors);
            return badRequestApiResponse;
        }
    }
}
