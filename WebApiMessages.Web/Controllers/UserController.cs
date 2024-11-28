using Microsoft.AspNetCore.Mvc;
using Messages.Bll.ModelsBll;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Messages.Web.Mappings;
using Messages.Bll.Interfaces;
using Messages.Web.Configurations;

namespace Messages.Web.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    Mapper _mapper;
    public UserController(IUserService userService)
    {
        _userService = userService;

        var config = new MapperConfiguration(
        cfg =>
        {
            cfg.AddProfile(new UserMapperProfile());
        });

        _mapper = new Mapper(config);
    }
    
    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUserAsync([FromBody] RegistrationUserRequest modelRegister)
    {
        if (modelRegister is null)
        {
            return BadRequest("Invalid client request");
        }
        var result = _mapper.Map<RegisterDto>(modelRegister);
        var user = await _userService.CreateUserAsync(result);
        
        if (user != null)
        {
            var newUser = _mapper.Map<UserResponse>(user);
            newUser.Token = JWT.GetToken();
            return Ok(newUser);
        }

        return StatusCode(500);
    }

    [HttpPost("auth")] 
    public async Task<ActionResult<UserResponse>> AuthenticateUserAsync([FromBody] AuthUserRequest authData)
    {
        if (authData is null)
        {
            return BadRequest("Invalid client request");
        }

        var result = _mapper.Map<AuthenticateDto>(authData);
        var user = await _userService.AuthenticateUserAsync(result);

        if (user != null)
        {
            var verifiedUser = _mapper.Map<UserResponse>(user);
            verifiedUser.Token = JWT.GetToken();
            return Ok(verifiedUser);
        }

        return Unauthorized();
    }

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
