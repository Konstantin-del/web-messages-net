using Microsoft.AspNetCore.Mvc;
using Messages.Core.Models.Requests;
using Messages.Core.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace Messages.Web.Controllers;

[Route("api/contacts")]
[ApiController, Authorize]
public class ContactsController : Controller
{
    [HttpPost("{id}")]
    public ActionResult Create([FromRoute] Guid id, [FromBody] CreateContactRequest contact)
    {
        try
        {
            return NoContent();
        }
        catch
        {
            return View();
        }
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