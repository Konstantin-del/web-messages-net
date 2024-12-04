using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Messages.Dal;

public class ContactRepository(Context context) : IContactRepository
{
    public async Task<bool> IsContactByIdAsync(Guid idOvner, Guid idRecipient)
    {
        var contact = await context.Contacts.FirstOrDefaultAsync(i => i.OwnerId == idOvner && i.RecipientId == idRecipient);
        if (contact is null) return false;
        return true;
    }

    public async Task<ContactEntity> AddContactAsync(ContactEntity contact)
    {
        await context.Contacts.AddAsync(contact);
        await context.SaveChangesAsync();
        //var result = await context.Contacts.FirstOrDefaultAsync(n => n.Id == id);
        return contact;
    }

    public async Task<List<ContactEntity>> GetContactByIdOwnerAsync(Guid idOwner)
    {
        var contacts = await context.Contacts.Where(i => i.OwnerId == idOwner).ToListAsync();
        return contacts;
    }
}
