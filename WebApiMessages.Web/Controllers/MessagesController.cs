using Microsoft.AspNetCore.Mvc;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace Messages.Web.Controllers;

[Route("api/messages")]
[ApiController, Authorize]
public class MessagesController : ControllerBase
{
    [HttpPost("{id}")]
    public ActionResult Message([FromRoute] Guid id, [FromBody] MessageRequest message)
    {
        return Ok(); 
    }

    [HttpGet("{id}")]
    public ActionResult<GetMessagesResponse> GetMessages([FromRoute] Guid id)
    {
        GetMessagesResponse Messages = new();
        return Ok(Messages); 
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        return NoContent();
    }
}

