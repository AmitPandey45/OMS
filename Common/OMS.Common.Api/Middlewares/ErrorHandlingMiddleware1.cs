using OMS.Common.Api.Helpers;
using System.Net;

namespace OMS.Common.Api.Middlewares
{
    public class ErrorHandlingMiddleware1
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware1> _logger;

        public ErrorHandlingMiddleware1(RequestDelegate next, ILogger<ErrorHandlingMiddleware1> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Log the exception
            _logger.LogError(ex, "An unexpected error occurred");

            // Create a standardized error response
            var apiResponse = ResponseHelper.CreateErrorResponse(
                "ErrorHandlingMiddleware.HandleExceptionAsync",
                "An unexpected error occurred. Please try again later."
            );

            // Set response details
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(apiResponse);
        }
    }
}
