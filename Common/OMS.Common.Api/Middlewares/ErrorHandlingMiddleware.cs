namespace OMS.Common.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception with context (e.g., unique request ID, etc.)
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

                // Optionally, set the response status code for exceptions
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // You can also return a custom error message in the response
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }

}
