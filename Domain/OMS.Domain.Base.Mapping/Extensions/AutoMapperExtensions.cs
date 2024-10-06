using AutoMapper;

namespace OMS.Domain.Base.Mapping.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void AddProfile<T>(this IMapperConfigurationExpression cfg, T profile) where T : Profile
        {
            cfg.AddProfile(profile);
        }

        public static void AddProfiles(this IMapperConfigurationExpression cfg, params Profile[] profiles)
        {
            foreach (var profile in profiles)
            {
                cfg.AddProfile(profile);
            }
        }
    }
}
