using AutoMapper;
using Messages.Bll.Exceptions;
using Messages.Bll.Interfaces;
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;

namespace Messages.Bll
{
    public class MessageService(
        IMessageRepository messageRepository,
        IUserRepository userRepository,
        IMapper mapper
        ) : IMessageService
    {
        public async Task<int> AddMessageAsync(MessageDto contact)
        {
            var result = mapper.Map<MessageEntity>(contact);
            result.Sender = await userRepository.GetUserByIdAsync(contact.SenderId);
            result.Recipiend = await userRepository.GetUserByIdAsync(contact.RecipiendId);
            result.SendDate = DateTimeOffset.UtcNow;
            var id = await messageRepository.AddMessageAsync(result);
            if(id<1)
                throw new FailedToCreateException("failed to add message");
            return id;
        }

        public async Task<List<MessageDto>> GetAllMessageFromContact(Guid SenderId, Guid RecipiendId)
        {
            var messages = await messageRepository.GetAllMessageFromContact(SenderId, RecipiendId);
            if(messages is null)
                throw new EntityNotFoundException("messages not found");
            var result = mapper.Map<List<MessageDto>>(messages);
            return result;
        }
    }
}
