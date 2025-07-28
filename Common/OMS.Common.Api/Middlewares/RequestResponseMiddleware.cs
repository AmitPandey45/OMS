namespace OMS.Common.Api.Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseMiddleware> _logger;

        public RequestResponseMiddleware(RequestDelegate next, ILogger<RequestResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the incoming request
            _logger.LogInformation("Received Request: {Method} {Path} {QueryString}",
                context.Request.Method, context.Request.Path, context.Request.QueryString);

            // Capture the original body stream
            var originalResponseBody = context.Response.Body;

            try
            {
                // Create a memory stream to capture the response body
                using (var responseBodyStream = new MemoryStream())
                {
                    context.Response.Body = responseBodyStream;

                    // Call the next middleware in the pipeline
                    await _next(context);

                    // Log the outgoing response
                    _logger.LogInformation("Response Sent: {StatusCode} {ContentType}",
                        context.Response.StatusCode, context.Response.ContentType);

                    // Copy the captured response body back to the original stream
                    await responseBodyStream.CopyToAsync(originalResponseBody);
                }
            }
            catch (Exception ex)
            {
                // Optionally handle logging for exceptions that occur during request processing here
                _logger.LogError(ex, "Error occurred while processing the request.");
                throw; // Let the error be caught by the ErrorHandlingMiddleware
            }
        }
    }
}
