namespace OMS.Common.Api.Extensions
{
    public static class CommonServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpContextAccessorService(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
