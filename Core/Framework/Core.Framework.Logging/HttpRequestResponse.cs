namespace Core.Framework.Logging
{
    public class HttpRequestResponse
    {
        public int StatusCode { get; set; }

        public string ContentType { get; set; }

        public Dictionary<string, string> Headers { get; set; }
    }
}
