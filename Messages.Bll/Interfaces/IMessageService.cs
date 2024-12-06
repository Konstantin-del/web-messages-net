
using Messages.Bll.ModelsBll;

namespace Messages.Bll.Interfaces;

public interface IMessageService
{
    Task<int> AddMessageAsync(MessageDto contact);

    Task<List<MessageDto>> GetAllMessageFromContact(Guid OwnerId, Guid Recipiend);
}
