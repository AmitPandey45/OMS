namespace OMS.Common.Extensions
{
    public static class DictionaryExtensions
    {
        public static string GetMessage(this Dictionary<int, string> dictionary, int code, string unknownMessage = "Unknown Error")
        {
            return dictionary.TryGetValue(code, out var message) ? message : unknownMessage;
        }
    }
}
