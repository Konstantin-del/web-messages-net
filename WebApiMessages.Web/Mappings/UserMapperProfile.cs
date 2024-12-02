using AutoMapper;
using Messages.Bll.ModelsBll;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;

namespace Messages.Web.Mappings;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegistrationUserRequest, RegisterDto>();
        CreateMap<UserDto, UserResponse>();
        CreateMap<AuthUserRequest, AuthenticateDto>();
        CreateMap<UpdateUserRequest, UpdateUserDto>();
        CreateMap<UpdateUserDto, UpdateUserResponse>();
        //.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id));
    }
}
