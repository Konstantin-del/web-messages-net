
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;

namespace Messages.Bll.Interfaces;

public interface IMessageService
{
    Task<int> AddMessageAsync(MessageDto contact);

    Task<List<MessageDto>> GetAllMessageFromContactAsync(Guid recipiendId, Guid senderId);

    Task<List<MessageDto>> GetAllUndeliveredMessagesAsync(Guid recipiendId);

    Task DeleteMessageAsync(Guid ownerId, int id);
}
