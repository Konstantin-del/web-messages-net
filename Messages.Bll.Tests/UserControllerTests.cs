
using AutoMapper;
using Messages.Bll.Interfaces;
using Messages.Web.Mappings;
using Messages.Bll.ModelsBll;
using Messages.Web.Controllers;
using Messages.Web.Models.Requests;
using Messages.Web.Models.Responses;
using Moq;
using Messages.Dal.Entityes;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using Messages.Web.Utils;

namespace Messages.Bll.Tests;

public class UserControllerTests 
{
    private readonly Mock<IUserService> _userServiceMock;
    private Mapper _mapper;
    private readonly UserController _sut;
    private MapperConfiguration _config;

    public UserControllerTests()
    {
        _config = new MapperConfiguration(
           cfg => cfg.AddProfile(new UserMapperProfile())
        );

        _mapper = new Mapper(_config);
        _userServiceMock = new Mock<IUserService>();
        _sut = new UserController(_userServiceMock.Object, _mapper);
    }

    [Fact]
    public async Task CreateUserAsync_ActionExecutes_ReturnUser()
    {
        // Arrange
        var registrationUser = new RegistrationUserRequest() { Name = "name"};
        var userDto = new UserDto() { Name = "name" };
        _userServiceMock.Setup(t => t.CreateUserAsync(It.Is<RegisterDto>(
            n => n.Name == registrationUser.Name)))
            .ReturnsAsync(userDto);
        // Act
        var result = await _sut.CreateUserAsync(registrationUser);
        // Assept
        Assert.IsType <ActionResult<UserResponse>>(result); 
        Assert.Equal(((result.Result as OkObjectResult)
            .Value as UserResponse).Name, registrationUser.Name);
    }

    [Fact]
    public async Task AuthenticateUserAsync_ActionExecutes_ReturnUser()
    {
        // Arrange
        var authUserRequest = new AuthUserRequest();
        var authenticateDto = new AuthenticateDto();
        var userDto = new UserDto() { Name = "name"};
        _userServiceMock.Setup(t => t.AuthenticateUserAsync(
            It.Is<AuthenticateDto>(n => n.Nick == authUserRequest.Nick)))
            .ReturnsAsync(userDto);
        // Act
        var result = await _sut.AuthenticateUserAsync(authUserRequest);
        // Assert
        Assert.IsType<ActionResult<UserResponse>>(result);
        Assert.Equal(((result.Result as OkObjectResult).Value as UserResponse).Name, "name");
    }
}
