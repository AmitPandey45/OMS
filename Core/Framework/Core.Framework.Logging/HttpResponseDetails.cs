namespace Core.Framework.Logging
{
    public class HttpResponseDetails
    {
        public int StatusCode { get; set; }

        public string ContentType { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public string Body { get; set; }
    }
}
