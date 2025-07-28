using Core.Framework.Logging;

namespace OMS.Common.Api.Logging
{
    public class HttpContextLogContext : ILogContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextLogContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UniqueRequestId => _httpContextAccessor.HttpContext?.TraceIdentifier;

        public string UserId => _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Anonymous";

        public string Api => _httpContextAccessor.HttpContext?.Request?.Path.ToString();

        public string HttpMethod => _httpContextAccessor.HttpContext?.Request?.Method;

        public int? HttpStatusCode => _httpContextAccessor.HttpContext?.Response?.StatusCode;

        public string UserAgent => _httpContextAccessor.HttpContext?.Request?.Headers["User-Agent"].ToString();

        public string IpAddress
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context == null) return null;

                var remoteIp = context.Connection.RemoteIpAddress;
                if (remoteIp != null)
                {
                    if (remoteIp.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 && remoteIp.IsIPv4MappedToIPv6)
                    {
                        return remoteIp.MapToIPv4().ToString();
                    }
                    return remoteIp.ToString();
                }

                var forwardedFor = context.Request.Headers["X-Forwarded-For"].ToString();
                if (!string.IsNullOrEmpty(forwardedFor))
                {
                    return forwardedFor.Split(',')[0].Trim();
                }

                return null;
            }
        }

        // Updated to use the new HttpRequestDetails and HttpResponseDetails classes
        public HttpRequestDetails Request => new HttpRequestDetails
        {
            Method = _httpContextAccessor.HttpContext?.Request.Method,
            Path = _httpContextAccessor.HttpContext?.Request.Path,
            QueryString = _httpContextAccessor.HttpContext?.Request.QueryString.ToString(),
            Headers = _httpContextAccessor.HttpContext?.Request.Headers
                .ToDictionary(h => h.Key, h => string.Join(", ", h.Value))
        };

        public HttpResponseDetails Response => new HttpResponseDetails
        {
            StatusCode = _httpContextAccessor.HttpContext?.Response?.StatusCode ?? 0,
            ContentType = _httpContextAccessor.HttpContext?.Response?.ContentType,
            Headers = _httpContextAccessor.HttpContext?.Response?.Headers
                .ToDictionary(h => h.Key, h => string.Join(", ", h.Value)),
        };

        // This method is used to set the response body from the middleware
        public void SetResponseBody(string body)
        {
            // Here we update the response body
            Response.Body = body;
        }
    }
}