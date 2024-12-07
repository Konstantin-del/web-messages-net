using Messages.Dal.Entityes;
using Messages.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messages.Dal;

public class ContactRepository(Context context) : IContactRepository
{
    public async Task<bool> IsContactByIdAsync(Guid idOvner, Guid idRecipient)
    {
        var contact = await context.Contacts.FirstOrDefaultAsync(
            i => i.OwnerId == idOvner && i.RecipiendId == idRecipient);
        if (contact is null) return false;
        return true;
    }

    public async Task<ContactEntity> AddContactAsync(ContactEntity contact)
    {
        await context.Contacts.AddAsync(contact);
        await context.SaveChangesAsync();
        var result = await context.Contacts.FirstOrDefaultAsync(
            n => n.OwnerId == contact.OwnerId && n.RecipiendId == contact.RecipiendId);
        return result;
    }

    public async Task<List<ContactEntity>> GetContactByIdOwnerAsync(Guid idOwner)
    {
        var contacts = await context.Contacts.Where(i => i.OwnerId == idOwner).ToListAsync();
        return contacts;
    }

    public async Task<ContactEntity> UpdateContactAsync(ContactEntity updateContact)
    {
        var contact = await context.Contacts.FirstOrDefaultAsync(
           n => n.OwnerId == updateContact.OwnerId && n.RecipiendId == updateContact.RecipiendId);
        contact.NameContact = updateContact.NameContact;
        await context.SaveChangesAsync();
        var newContact = await context.Contacts.FirstOrDefaultAsync(
           n => n.OwnerId == contact.OwnerId && n.RecipiendId == contact.RecipiendId);
        return newContact;
    }

    public async Task DeleteContactAsync(Guid idOwner, Guid idRecipient)
    {
        await context.Contacts.Where(n => 
            n.OwnerId == idOwner && n.RecipiendId == idRecipient
        ).ExecuteDeleteAsync();
    }
}
