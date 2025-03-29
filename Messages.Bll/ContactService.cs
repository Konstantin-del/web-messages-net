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
       // bool isExists = contactRepository.IsContactByIdAsync(contact.OwnerId, contact.RecipiendId).Result;
       // if (isExists)
       //     throw new UserAlreadyExistsException("this contact already exists");
        var result = mapper.Map<ContactEntity>(contact);
        result.Owner = await userRepository.GetUserByIdAsync(contact.OwnerId);
        var newContact = await contactRepository.AddContactAsync(result);
        if(newContact is null) 
            throw new FailedToCreateException("failed to create contact");
        var item = mapper.Map<ContactDto>(newContact);
        return item;
    }

    public async Task<List<ContactDto>> GetContactByIdOwnerAsync(Guid idOwner)
    {
        var items = await contactRepository.GetContactByIdOwnerAsync(idOwner);
        if(items is null)
            throw new EntityNotFoundException("contacts does not found");
        var contacts = mapper.Map<List<ContactDto>>(items);
        return contacts;
    }
     
    public async Task<List<InfoForConnectDto>> GetContactByNickAsync(string nick)
    {
        var result = await contactRepository.GetListUserByNickAsync(nick); 
        if (result is null)
            throw new EntityNotFoundException("contacts does not found");
        var contacts = mapper.Map<List<InfoForConnectDto>>(result);
        return contacts;
    }

    public async Task<ContactDto> UpdateContactAsync(ContactDto updateContact)
    {
        bool isExists = contactRepository.IsContactByIdAsync(updateContact.OwnerId, updateContact.RecipiendId).Result;
        if (!isExists)
            throw new EntityNotFoundException("contacts does not found");
        var contact = mapper.Map<ContactEntity>(updateContact);
        var newContact = await contactRepository.UpdateContactAsync(contact);
        var result = mapper.Map<ContactDto>(newContact);
        return result;
    }

    public async Task DeleteContactAsync(Guid idOwner, Guid idRecipiend)
    {
        bool isExists = contactRepository.IsContactByIdAsync(idOwner, idRecipiend).Result;
        if (!isExists)
            throw new EntityNotFoundException("contacts does not found");
        await contactRepository.DeleteContactAsync(idOwner, idRecipiend);
    }
}
