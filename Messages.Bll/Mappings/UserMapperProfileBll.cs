using AutoMapper;
using Messages.Dal.Entityes;
using Messages.Bll.ModelsBll;

namespace Messages.Bll.Mappings;
public class UserMapperProfileBll : Profile
{
    public UserMapperProfileBll()
    {
        CreateMap<RegisterDto, UserEntity>();
        CreateMap<UserEntity, UserDto>().ReverseMap();
        CreateMap<UpdateUserDto, UpdateUserEntity>().ReverseMap();
        //.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id))
    }
}
