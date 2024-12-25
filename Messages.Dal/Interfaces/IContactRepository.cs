using Messages.Dal.Entityes;

namespace Messages.Dal.Interfaces
{
    public interface IContactRepository
    {
        Task<ContactEntity> AddContactAsync(ContactEntity contact);

        Task<bool> IsContactByIdAsync(Guid idOvner, Guid idRecipient);

        Task<List<ContactEntity>> GetContactByIdOwnerAsync(Guid idOwner);

        Task DeleteContactAsync(Guid idOwner, Guid idRecipient);

        Task<ContactEntity> UpdateContactAsync(ContactEntity contact);

        Task<List<UserEntity>> GetListUserByNickAsync(string nick);
    }
}
