using OMS.Domain.Base.Mapping.MappingProfiles;
using OMS.Domain.User.Dto;
using OMS.Domain.User.Dto.User;
using OMS.Domain.User.Entity;
using OMS.Domain.User.Entity.User;

namespace OMS.Domain.User.Mapping.MappingProfiles
{
    public class UserEntityToUserDtoMappingProfile : BaseProfile
    {
        public UserEntityToUserDtoMappingProfile()
        {
            this.CreateMap<BaseDto, BaseEntity>().ReverseMap();
            this.CreateMap<UserEntity, GetUserResponseDto>();
            this.CreateMap<CreateUserRequestDto, UserEntity>();
            this.CreateMap<UpdateUserRequestDto, UserEntity>();
        }
    }
}
