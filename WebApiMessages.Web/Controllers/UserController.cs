using Microsoft.AspNetCore.Mvc;
using Messages.Bll.ModelsBll;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Messages.Bll.Interfaces;
using Messages.Web.Utils;
using Microsoft.Net.Http.Headers;

namespace Messages.Web.Controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService userService,IMapper mapper) : Controller
{ 
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUserAsync([FromBody] RegistrationUserRequest modelRegister)
    {
        var result = mapper.Map<RegisterDto>(modelRegister);
        var user = await userService.CreateUserAsync(result);
        var newUser = mapper.Map<UserResponse>(user);
        newUser.Token = JWT.GetToken(user.Id.ToString());
        return Ok(newUser);
    }

    [HttpPost("auth")] 
    public async Task<ActionResult<UserResponse>> AuthenticateUserAsync([FromBody] AuthUserRequest authData)
    {
        var result = mapper.Map<AuthenticateDto>(authData);
        var user = await userService.AuthenticateUserAsync(result);
        try
        {
            var verifiedUser = mapper.Map<UserResponse>(user);
            verifiedUser.Token = JWT.GetToken(user.Id.ToString());
            return Ok(verifiedUser);
        }
        catch 
        {
            return Unauthorized();
        }
    }

    [HttpPatch(), Authorize]
    public async Task<ActionResult<UpdateUserResponse>> EditUser([FromBody] UpdateUserRequest modelUpdate)
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0,7);
        var id = JWT.DecodeJwtAndReturnId(accessToken);
        if (id == Guid.Empty) return BadRequest();
        var result = mapper.Map<UpdateUserDto>(modelUpdate);
        var newItem = await userService.UpdateUserAsync(id, result);
        return Ok(newItem);
    }

    [HttpDelete(), Authorize]
    public async Task<ActionResult> DeleteUser()
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0, 7);
        var id = JWT.DecodeJwtAndReturnId(accessToken);
        if (id == Guid.Empty) return BadRequest();
        await userService.DeleteUserAsync(id);
        return NoContent();
    }
}
