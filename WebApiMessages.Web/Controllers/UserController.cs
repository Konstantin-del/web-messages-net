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
    private UserService? _userService { get; set; }
    private JWT? _jwt { get; set; }

    [HttpPost]
    public ActionResult <UserResponse> Create([FromBody] RegistrationUserRequest modelRegister)
    {
        if (modelRegister is null)
        {
            return BadRequest("Invalid client request");
        }

        _userService = new();
        var user = _userService.CreateUser(modelRegister);

        if (user != null)
        {
            _jwt = new();
            user.Token = _jwt.getToken();
            return Ok(user);
        }

        return StatusCode(500);
    }

    [HttpPost("auth")] 
    public ActionResult<UserResponse> Auth([FromBody] AuthUserRequest authData)
    {
        if (authData is null)
        {
            return BadRequest("Invalid client request");
        }

        _userService = new();
        var user = _userService.UserAuth(authData);

        if (user != null)
        {
            _jwt = new();
            user.Token = _jwt.getToken();
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
