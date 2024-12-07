
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

    public async Task<List<MessageEntity>> GetAllMessageFromContactAsync(Guid SenderId, Guid RecipiendId)
    {
        return await context.Messages.Where( n =>
           n.RecipiendId == RecipiendId && 
           n.SenderId == SenderId ||
           n.RecipiendId == SenderId &&
           n.SenderId == RecipiendId
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

    public async Task<List<MessageEntity>> GetAllUndeliveredMessagesAsync(Guid recipiendId)
    {
        return await context.Messages.Where( n =>
           n.RecipiendId == recipiendId && 
           n.IsDelivered == false
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
