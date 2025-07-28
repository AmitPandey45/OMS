using Core.Framework.Logging;
using OMS.Common.Api.Logging;

namespace OMS.Common.Api.Middlewares
{
    public class ResponseBodyCaptureMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ResponseBodyCaptureMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            // Create a memory stream to capture the response body
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                // Continue with the request processing
                await _next(context);

                // Read the response body from the memory stream
                memoryStream.Seek(0, SeekOrigin.Begin);
                var body = await new StreamReader(memoryStream).ReadToEndAsync();

                // Set the captured body into the HttpContextLogContext
                var logContext = _httpContextAccessor.HttpContext?.RequestServices.GetService<ILogContext>() as HttpContextLogContext;
                logContext?.SetResponseBody(body);  // Save the body into the context

                // Ensure that the original stream is written to the client
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalBodyStream);
            }
        }
    }
}
