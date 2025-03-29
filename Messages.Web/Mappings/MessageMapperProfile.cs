using AutoMapper;
using Messages.Bll.ModelsBll;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;

namespace Messages.Web.Mappings;

public class MessageMapperProfile : Profile
{
    public MessageMapperProfile()
    {
        CreateMap<AddMessageRequest, MessageDto>().ReverseMap();
        CreateMap<MessageDto, GetMessagesResponse>();
    }
}
