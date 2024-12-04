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
        CreateMap<ContactEntity, ContactDto>().ReverseMap();
        CreateMap<UpdateUserDto, UpdateUserEntity>();
        CreateMap<UserDto, UpdateUserDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }

}
