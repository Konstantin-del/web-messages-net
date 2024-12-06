
using AutoMapper;
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;

namespace Messages.Bll.Mappings;

public class MessageMapperProfileBll : Profile
{
    public MessageMapperProfileBll()
    {
        CreateMap<MessageEntity, MessageDto>().ReverseMap();
    }
}
