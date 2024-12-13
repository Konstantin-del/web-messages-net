using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;

namespace Messages.Bll.Interfaces;

public interface IContactService
{
    Task<ContactDto> AddContactAsync(ContactDto contact);

    Task<List<ContactDto>> GetContactByIdOwnerAsync(Guid idOwner);

    Task<Guid> GetContactByNickAsync(string nick);

    Task DeleteContactAsync(Guid idOwner, Guid idRecipient);

    Task<ContactDto> UpdateContactAsync(ContactDto contact);
}
