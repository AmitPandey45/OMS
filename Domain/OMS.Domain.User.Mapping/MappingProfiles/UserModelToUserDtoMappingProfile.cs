using OMS.Domain.Base.Mapping.MappingProfiles;
using OMS.Domain.User.Dto;
using OMS.Domain.User.Dto.User;
using OMS.Domain.User.Model;
using OMS.Domain.User.Model.User;

namespace OMS.Domain.User.Mapping.MappingProfiles
{
    public class UserModelToUserDtoMappingProfile : BaseProfile
    {
        public UserModelToUserDtoMappingProfile()
        {
            this.CreateMap<BaseModel, BaseDto>().ReverseMap();
            this.CreateMap<CreateUserRequestModel, CreateUserRequestDto>();
            this.CreateMap<GetUserResponseDto, GetUserResponseModel>();
            this.CreateMap<UpdateUserRequestModel, UpdateUserRequestDto>();
        }
    }
}
