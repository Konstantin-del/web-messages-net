using Microsoft.AspNetCore.Mvc;
using Messages.Core.Models.Requests;
using Messages.Core.Models.Responses;
using Messages.Bll;
using Microsoft.AspNetCore.Authorization;


namespace Messages.Web.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    [HttpPost]
    public ActionResult <UserResponse> Create(RegistrationUserRequest modelRegister)
    {
        try
        {

            UserResponse user = new();
            user.Token = JWT.getToken();
            return Ok(user);
        }
        catch
        {
            return View();
        }
    }

    [HttpPost("auth")] 
    public ActionResult<UserResponse> Auth(AuthUserRequest authData)
    {
        if (authData is null)
        {
            return BadRequest("Invalid client request");
        }

        UserService userService = new();
        var user = userService.GetUser(authData);

        if (user != null)
        {
            user.Token = JWT.getToken();
            return Ok( user );
        }

        return Unauthorized();
    }

    //[HttpGet, Authorize]
    //public ActionResult<UserDataResponse> GetUser([FromBody] string Nick)
    //{
    //    try
    //    {
    //        return Ok();
    //    }
    //    catch
    //    {
    //        return View();
    //    }
    //}

    [HttpPut("{id}"), Authorize]    
    public ActionResult Edit([FromRoute] Guid id, [FromBody] UpdateUserRequest modelUpdate)
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

    [HttpDelete("{id}"), Authorize]
    public ActionResult Delete([FromRoute] Guid id)
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
}
