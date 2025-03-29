
using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messages.Dal;

public class MessageRepository(Context context) : IMessageRepository
{
    public async Task<int> AddMessageAsync(MessageEntity message)
    {
        await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();
        return message.Id;
    }

    public async Task<List<MessageEntity>> GetAllMessageFromContactAsync(Guid senderId, Guid recipiendId)
    {
        return await context.Messages.Where( n =>
           n.RecipiendId == recipiendId && 
           n.SenderId == senderId ||
           n.RecipiendId == senderId &&
           n.SenderId == recipiendId
        ).ToListAsync();
    }

    public async Task UpdateIsDeliveredToTrue(Guid recipiendId)
    {
        await context.Messages.Where( n =>
           n.RecipiendId == recipiendId)
           .ExecuteUpdateAsync( s => 
           s.SetProperty(x => x.IsDelivered, x => true)
        );
    }

    public async Task<List<MessagePlusNickEntity>> GetAllUndeliveredMessagesAsync(Guid recipiendId)
    {
        return await context.Messages.Join(context.Users,
            m => m.SenderId,
            u => u.Id,
            (m, u)=> new 
            {
                id = m.Id,
                message = m.Message,
                sendDate = m.SendDate,
                recipiendId = m.RecipiendId,
                senderId = m.SenderId,
                isDelivered = m.IsDelivered,
                nick = u.Nick
                
            }).Where(n =>
            n.recipiendId == recipiendId &&
            n.isDelivered == false
        ).Select(m => new MessagePlusNickEntity()
        {
            Id = m.id,
            Message = m.message,
            SendDate = m.sendDate,
            RecipiendId = m.recipiendId,
            SenderId = m.senderId,
            Nick = m.nick
        }
        ).ToListAsync();
    }

    public async Task DeleteMessageAsync(Guid ownerId, int id)
    {
        await context.Messages.Where(n =>
            n.SenderId == ownerId && n.Id == id).ExecuteDeleteAsync();
    }

    public async Task<bool> GetMessageByOwnerIdAndIdAsync(Guid ownerId, int id)
    {
        var result = await context.Messages.Where( n =>
            n.SenderId == ownerId &&
            n.Id == id
        ).FirstOrDefaultAsync();
        if (result is null) return false;
        return true;
    }

}
