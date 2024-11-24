using Microsoft.AspNetCore.Mvc;
using Messages.Bll;
using Messages.Bll.ModelsBll;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Messages.Web.Mappings;

namespace Messages.Web.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    UserService _userService;
    JWT _jwt;
    Mapper _mapper;
    public UserController()
    {
        _jwt = new();
        _userService = new();

        var config = new MapperConfiguration(
        cfg =>
        {
            cfg.AddProfile(new UserMapperProfile());
        });

        _mapper = new Mapper(config);
    }
    
    [HttpPost]
    public ActionResult <UserResponse> Create([FromBody] RegistrationUserRequest modelRegister)
    {
        if (modelRegister is null)
        {
            return BadRequest("Invalid client request");
        }
        var result = _mapper.Map<RegisterBll>(modelRegister);
        var user = _userService.CreateUser(result);
        
        if (user != null)
        {
            var newUser = _mapper.Map<UserResponse>(user);
            newUser.Token = _jwt.getToken();
            return Ok(newUser);
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
        var result = _mapper.Map<AuthBll>(authData);
        var user = _userService.UserAuth(result);

        if (user != null)
        {
            var verifiedUser = _mapper.Map<UserResponse>(user);
            verifiedUser.Token = _jwt.getToken();
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
