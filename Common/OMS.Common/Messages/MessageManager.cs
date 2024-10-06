using OMS.Common.MessageProviders;
using OMS.Common.Messages.Http;
using OMS.Common.Messages.User;

namespace OMS.Common.Messages
{
    public static class MessageManager
    {
        public const string HttpMessageProvider = "Http";
        public const string UserResponseProvider = "User";

        private static readonly Dictionary<string, IResponseMessageProvider> _providers = new Dictionary<string, IResponseMessageProvider>
        {
            { HttpMessageProvider, new HttpResponseMessages() },
            { UserResponseProvider, new UserResponseMessages() },
        };

        public static string GetMessage(int code, string requestedProvider)
        {
            var requestedProviders = new HashSet<string>
            {
                HttpMessageProvider,
                requestedProvider,
            };

            return GetMessage(code, requestedProviders);
        }

        public static string GetMessage(int code, IEnumerable<string> requestedProviders)
        {
            var providerSet = new HashSet<string> { HttpMessageProvider };
            foreach (var provider in requestedProviders)
            {
                providerSet.Add(provider);
            }

            foreach (string providerKey in providerSet)
            {
                if (_providers.TryGetValue(providerKey, out var provider))
                {
                    return provider.GetMessage(code);
                }
            }

            return "Unknown Error";
        }
    }
}
