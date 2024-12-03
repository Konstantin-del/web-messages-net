using Microsoft.AspNetCore.Mvc;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Messages.Web.Utils;
using Microsoft.Net.Http.Headers;
using AutoMapper;
using Messages.Bll.Interfaces;

namespace Messages.Web.Controllers;

[Route("api/contacts")]
[ApiController, Authorize]
public class ContactsController(IMapper mapper, IContactService contactService ) : Controller
{
    [HttpPost()]
    public ActionResult Create( [FromBody] CreateContactRequest contact)
    {
        //string accessToken = Request.Headers[HeaderNames.Authorization];
        //accessToken = accessToken.Remove(0, 7);
        //var id = JWT.DecodeJwtAndReturnId(accessToken);
        //if (id == Guid.Empty) return BadRequest();

        return View();
    }

    [HttpGet("{id}")]
    public ActionResult<GetContactsUserResponse> getContacts([FromRoute] Guid id)
    {
        try
        {
            GetContactsUserResponse contacts = new();
            return Ok(contacts);
        }
        catch
        {
            return View();
        }
    }

    [HttpPut("{id}")]
    public ActionResult Edit([FromRoute] Guid id, [FromBody] UpdateContactRequest nameContact)
    {
        UpdateContactRequest contactName = new();

        return Ok(contactName);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] Guid id)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}