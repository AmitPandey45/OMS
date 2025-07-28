namespace Core.Framework.Logging
{
    public interface ILogContext
    {
        string UniqueRequestId { get; }

        string UserId { get; }

        string Api { get; }

        string HttpMethod { get; }

        int? HttpStatusCode { get; }

        string UserAgent { get; }

        string IpAddress { get; }

        HttpRequestDetails Request { get; }

        HttpResponseDetails Response { get; }
    }
}
