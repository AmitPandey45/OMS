using OMS.Common.Api.Extensions;
using OMS.Common.Extensions;
using OMS.Repository.User.Implementation;
using OMS.Repository.User.Interface;
using OMS.Service.User.Implementation;
using OMS.Service.User.Interface;

namespace User.Api.Extensions
{
    public static class UserServiceCollectionExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddCommonServices();

            services
                .AddHttpContextAccessorService();
        }

        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
