using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OMS.Common.Api.Helpers;
using System.Net;
using System.Threading.Tasks;

namespace OMS.Common.Api.Middlewares
{
    public class CustomValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomValidationMiddleware> _logger;

        public CustomValidationMiddleware(RequestDelegate next, ILogger<CustomValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Items.TryGetValue("ModelState", out var modelStateObj) &&
                modelStateObj is ModelStateDictionary modelState)
            {
                var errors = modelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

                _logger.LogWarning("Validation errors occurred: {@Errors}", errors);
                // Create your ApiResponse
                var errorResponse = new ApiResponse<object>(
                    (int)HttpStatusCode.BadRequest,
                    "CustomValidationMiddleware.Invoke",
                    null, // No data returned
                    "One or more validation errors occurred."
                )
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Result = errors // Attach errors here, but keep in mind it's of type object
                };

                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(ResponseHelper.SerializeResponse(errorResponse));
            }
        }
    }
}
