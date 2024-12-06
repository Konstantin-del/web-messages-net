using Messages.Bll.ModelsBll;
using AutoMapper;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;

namespace Messages.Web.Mappings;

public class ContactMapperProfile : Profile
{
    public ContactMapperProfile()
    {
        CreateMap<AddContactRequest, ContactDto>().ReverseMap();
        CreateMap<ContactDto, GetContactsUserResponse>().ReverseMap();
        CreateMap<ContactDto, UpdateContactResponse>();
        CreateMap<UpdateContactRequest, ContactDto>();
    }
}
