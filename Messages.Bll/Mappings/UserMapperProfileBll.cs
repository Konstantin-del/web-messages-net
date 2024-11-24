using AutoMapper;
using Messages.Dal.Dtos;
using Messages.Bll.ModelsBll;

namespace Messages.Bll.Mappings;
public class UserMapperProfileBll : Profile
{
    public UserMapperProfileBll()
    {
        CreateMap<RegisterBll, UserDto>();
        CreateMap<UserDto, UserBll>().ReverseMap();
        //.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id))
    }
}
