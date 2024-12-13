using Microsoft.AspNetCore.Mvc;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Messages.Web.Utils;
using Microsoft.Net.Http.Headers;
using AutoMapper;
using Messages.Bll.Interfaces;
using Messages.Bll.ModelsBll;

namespace Messages.Web.Controllers;

[Route("api/contacts")]
[ApiController, Authorize]
public class ContactsController(IMapper mapper, IContactService contactService) : Controller
{
    [HttpPost()]
    public async Task<ActionResult<AddContactRespons>> AddContactAsync(
        [FromBody] AddContactRequest contact)
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0, 7);
        var id = JWT.DecodeJwtAndReturnId(accessToken);
        if (id == Guid.Empty) return BadRequest();
        var result = mapper.Map<ContactDto>(contact);
        result.OwnerId = id;
        var item = await contactService.AddContactAsync(result);
        return Ok(item);
    }

    [HttpGet]
    public async Task<ActionResult<List<GetContactsUserResponse>>> getContacts()
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0, 7);
        var id = JWT.DecodeJwtAndReturnId(accessToken);
        var items = await contactService.GetContactByIdOwnerAsync(id);
        var result = mapper.Map<List<GetContactsUserResponse>>(items);
        return result;
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Guid>> GetContact([FromRoute] string name)
    {
        var nick = name;
        var item = await contactService.GetContactByNickAsync(nick);
        return item;
    }

    [HttpPatch]
    public async Task<ActionResult<UpdateContactResponse>> EditContact(
        [FromBody] UpdateContactRequest updateContact)
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0, 7);
        var idOwner = JWT.DecodeJwtAndReturnId(accessToken);
        var result = mapper.Map<ContactDto>(updateContact);
        result.OwnerId = idOwner;
        var newContact = await contactService.UpdateContactAsync(result);
        var contact = mapper.Map<UpdateContactResponse>(newContact);
        return Ok(contact);
    }

    [HttpDelete()]
    public async Task<ActionResult> DeleteContact([FromBody] DeleteContactRequest RecipientId)
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0, 7);
        var idOwner = JWT.DecodeJwtAndReturnId(accessToken);
        await contactService.DeleteContactAsync(idOwner, RecipientId.IdRecipient);
        return NoContent();
    }
}