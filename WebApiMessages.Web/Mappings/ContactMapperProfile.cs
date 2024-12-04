using Messages.Bll.ModelsBll;
using AutoMapper;
using Messages.Web.Models.Requests;

namespace Messages.Web.Mappings;

public class ContactMapperProfile : Profile
{
    public ContactMapperProfile()
    {
        CreateMap<AddContactRequest, ContactDto>().ReverseMap();
    }
}
