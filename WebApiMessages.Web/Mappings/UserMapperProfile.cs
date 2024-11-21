using AutoMapper;
using Messages.Bll.ModelsBll;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;

namespace Messages.Web.Mappings
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegistrationUserRequest, RegisterBll>();
            CreateMap<UserBll, UserResponse>();
            CreateMap<AuthUserRequest, AuthBll>();  
            //.ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Role.Id));
            //ReverseMap() => с этим сможет работать в обратную сторону но formember не работает тогда
        }
    }
}
