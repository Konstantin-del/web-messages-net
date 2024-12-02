using Microsoft.AspNetCore.Mvc;
using Messages.Bll.ModelsBll;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Messages.Web.Mappings;
using Messages.Bll.Interfaces;
using Messages.Web.Configurations;
using Messages.Bll.Exceptions;
using Microsoft.Net.Http.Headers;
using System;

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
        var result = _mapper.Map<RegisterDto>(modelRegister);
        var user = await _userService.CreateUserAsync(result);
        var newUser = _mapper.Map<UserResponse>(user);
        newUser.Token = JWT.GetToken(newUser.Id.ToString());
        return Ok(newUser);
    }

    [HttpPost("auth")] 
    public async Task<ActionResult<UserResponse>> AuthenticateUserAsync([FromBody] AuthUserRequest authData)
    {
        var result = _mapper.Map<AuthenticateDto>(authData);
        var user = await _userService.AuthenticateUserAsync(result);
        try
        {
            var verifiedUser = _mapper.Map<UserResponse>(user);
            verifiedUser.Token = JWT.GetToken(user.Id.ToString());
            return Ok(verifiedUser);
        }
        catch 
        {
            return Unauthorized();
        }
    }

    [HttpPatch(), Authorize]
    public async Task<ActionResult<UpdateUserResponse>> Edit([FromBody] UpdateUserRequest modelUpdate)
    {
        string accessToken = Request.Headers[HeaderNames.Authorization];
        accessToken = accessToken.Remove(0,7);
        var id = JWT.DecodeJwtAndReturnId(accessToken);
        if (id == Guid.Empty) return BadRequest();
        var result = _mapper.Map<UpdateUserDto>(modelUpdate);
        var newItem = await _userService.UpdateUserAsync(id, result);
        return Ok(newItem);
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
