
using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messages.Dal
{
    public class MessageRepository(Context context) : IMessageRepository
    {
        public async Task<int> AddMessageAsync(MessageEntity message)
        {
            Console.WriteLine("repo");
            await context.Messages.AddAsync(message);
            await context.SaveChangesAsync();
            return message.Id;
        }

        public async Task<List<MessageEntity>> GetAllMessageFromContact(Guid SenderId, Guid RecipiendId)
        {
            
             var result = await context.Messages.Where(
                n => n.RecipiendId == RecipiendId && 
                n.SenderId == SenderId ||
                n.RecipiendId == SenderId &&
                n.SenderId == RecipiendId
            ).ToListAsync();
            Console.WriteLine(result[0].SendDate);
            return result;
        }
    }
}
