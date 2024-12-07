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

        public async Task<List<MessageDto>> GetAllMessageFromContactAsync(Guid senderId, Guid recipiendId)
        {
            var messages = await messageRepository.GetAllMessageFromContactAsync(senderId, recipiendId);
            if(messages is null)
                throw new EntityNotFoundException("messages not found");
            var result = mapper.Map<List<MessageDto>>(messages);

            await messageRepository.UpdateIsDeliveredToTrue(senderId);
            return result;
        }

        public async Task<List<MessageDto>> GetAllUndeliveredMessagesAsync(Guid recipiendId)
        {
            var messages = await messageRepository.GetAllUndeliveredMessagesAsync(recipiendId);
            if (messages is null)
                throw new EntityNotFoundException("messages not found");
            var result = mapper.Map<List<MessageDto>>(messages);
            await messageRepository.UpdateIsDeliveredToTrue(recipiendId);
            return result;
        }

        public async Task DeleteMessageAsync(Guid ownerId, int id)
        {
            bool isResult = messageRepository.GetMessageByOwnerIdAndIdAsync(ownerId, id).Result;
            if (!isResult)
                throw new EntityNotFoundException("messages not found");
            await messageRepository.DeleteMessageAsync(ownerId, id);
        }
    }
}
