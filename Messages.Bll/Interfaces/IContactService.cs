using Messages.Bll.ModelsBll;
using Messages.Dal.Entityes;

namespace Messages.Bll.Interfaces;

public interface IContactService
{
    Task<ContactDto> AddContactAsync(ContactDto contact);

    Task<List<ContactEntity>> GetContactByIdOwnerAsync(Guid idOwner);
}
