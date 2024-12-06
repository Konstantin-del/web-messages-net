
using Messages.Dal.Entityes;

namespace Messages.Dal.Interfaces;

public interface IMessageRepository
{
    Task<int> AddMessageAsync(MessageEntity message);

    Task<List<MessageEntity>> GetAllMessageFromContact(Guid OwnerId, Guid Recipiend);
}
