using AutoMapper;
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;

namespace Messages.Bll.Mappings;

public class ContactMapperProfileBll : Profile
{
    public ContactMapperProfileBll()
    {
        CreateMap<ContactEntity, ContactDto>().ReverseMap();
        CreateMap<UserEntity, InfoForConnectDto>().ReverseMap();
    }
}
