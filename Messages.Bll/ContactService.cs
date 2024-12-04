using AutoMapper;
using Messages.Bll.Exceptions;
using Messages.Bll.Interfaces;
using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;
namespace Messages.Bll;

public class ContactService(
    IMapper mapper, 
    IContactRepository contactRepository, 
    IUserRepository userRepository
) : IContactService
{
    public async Task<ContactDto> AddContactAsync(ContactDto contact)
    {
        bool isExists = contactRepository.IsContactByIdAsync(contact.OwnerId, contact.RecipientId).Result;
        if (isExists)
            throw new UserAlreadyExistsException("this contact already exists");
        var result = mapper.Map<ContactEntity>(contact);
        result.Owner = await userRepository.GetUserByIdAsync(contact.OwnerId);
        var newContact = await contactRepository.AddContactAsync(result);
        if(newContact is null) 
            throw new FailedToCreateException("failed to create contact");
        var item = mapper.Map<ContactDto>(newContact);
        return item;
    }

    public async Task<List<ContactEntity>> GetContactByIdOwnerAsync(Guid idOwner)
    {
        return await contactRepository.GetContactByIdOwnerAsync(idOwner);
    }




}
