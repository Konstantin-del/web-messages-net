using Microsoft.AspNetCore.Mvc;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Messages.Web.Utils;
using Microsoft.Net.Http.Headers;
using AutoMapper;
using Messages.Bll.ModelsBll;
using Messages.Bll.Interfaces;

namespace Messages.Web.Controllers;

[Route("api/messages")]
[ApiController, Authorize]
public class MessagesController(IMapper mapper, IMessageService messageService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> AddMessage([FromBody] AddMessageRequest message)
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0, 7);
        var SenderId = JWT.DecodeJwtAndReturnId(accessToken);
        var result = mapper.Map<MessageDto>(message);
        result.SenderId = SenderId;
        var id = await messageService.AddMessageAsync(result);
        return Ok(id);
    }

    [HttpGet("{id}/all")]
    public async Task<ActionResult<List<GetMessagesResponse>>> GetAllMessageFromContact([FromRoute] Guid id)
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0, 7);
        var IdSender = JWT.DecodeJwtAndReturnId(accessToken);
        var SenderId = IdSender;
        var messages = await messageService.GetAllMessageFromContact(SenderId, id);
        var result = mapper.Map <List<GetMessagesResponse>>(messages);
        return Ok(result);
    }

    //[HttpGet("{id}")]
    //public ActionResult<GetMessagesResponse> GetMessages([FromRoute] Guid id)
    //{
    //    GetMessagesResponse Messages = new();
    //    return Ok(Messages); 
    //}

    //[HttpDelete("{id}")]
    //public ActionResult Delete(int id)
    //{
    //    return NoContent();
    //}
}

