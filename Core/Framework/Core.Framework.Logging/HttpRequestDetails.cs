namespace Core.Framework.Logging
{
    public class HttpRequestDetails
    {
        public string Method { get; set; }

        public string Path { get; set; }

        public string QueryString { get; set; }

        public Dictionary<string, string> Headers { get; set; }
    }
}
