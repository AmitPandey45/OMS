using Microsoft.Extensions.DependencyInjection;
using OMS.Common.Security;

namespace OMS.Common.Extensions
{
    public static class CommonServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            return services;
        }
    }
}
