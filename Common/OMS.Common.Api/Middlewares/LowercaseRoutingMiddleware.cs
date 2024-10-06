namespace OMS.Common.Api.Middlewares
{
    public class LowercaseRoutingMiddleware
    {
        private readonly RequestDelegate _next;

        public LowercaseRoutingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.HasValue)
            {
                context.Request.Path = context.Request.Path.Value.ToLowerInvariant();
            }

            await _next(context);
        }
    }
}
