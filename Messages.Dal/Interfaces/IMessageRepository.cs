
using Messages.Dal.Entityes;

namespace Messages.Dal.Interfaces;

public interface IMessageRepository
{
    Task<int> AddMessageAsync(MessageEntity message);

    Task<List<MessageEntity>> GetAllMessageFromContactAsync(Guid recipiend, Guid senderId);

    Task UpdateIsDeliveredToTrue(Guid recipiendId);

    Task<List<MessagePlusNickEntity>> GetAllUndeliveredMessagesAsync(Guid recipiendId);

    Task DeleteMessageAsync(Guid ownerId, int id);

    Task<bool> GetMessageByOwnerIdAndIdAsync(Guid ownerId, int id);
}
