using Microsoft.AspNetCore.Mvc;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Messages.Web.Controllers;

[Route("api/messages")]
[ApiController, Authorize]
public class MessagesController : ControllerBase
{
    //[HttpGet]
    //public ActionResult GetAll()
    //{
    //    return Ok();
    //}

    //[HttpGet("{id}")]
    //public ActionResult Get(int id)
    //{
    //    return "value";
    //}

    [HttpPost("{id}")]
    public ActionResult Message([FromRoute] Guid id, [FromBody] MessageRequest message)
    {
        return Ok(); // how return succece
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

