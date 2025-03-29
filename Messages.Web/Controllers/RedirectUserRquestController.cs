using Messages.Bll.Interfaces;
using Messages.Bll.ModelsBll;
using Microsoft.AspNetCore.Mvc;

namespace Messages.Web.Controllers;

[Route("api/redirect")]
[ApiController]
public class RedirectUserRequestController(
    IRedirectUserRequestService redirectUserRequestService
    ) : Controller

{
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserFronPlaceholderAsync([FromRoute] int id)
    {
        var result = await redirectUserRequestService.GetUserFromJsonPlaceholderAsync(id);

        return Ok(result);
    }
}
