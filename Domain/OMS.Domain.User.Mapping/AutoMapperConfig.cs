using AutoMapper;
using OMS.Domain.Base.Mapping.Extensions;
using OMS.Domain.User.Mapping.MappingProfiles;

namespace OMS.Domain.User.Mapping
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
            {
                // cfg.AddProfile(new UserModelToUserDtoMappingProfile());
                // cfg.AddProfile(new UserEntityToUserDtoMappingProfile());
                cfg.AddProfiles(
                    new UserModelToUserDtoMappingProfile(),
                    new UserEntityToUserDtoMappingProfile()
                    );
            });
        }
    }
}
