using AutoMapper;
using Moq;
using Messages.Dal.Interfaces;
using Messages.Dal.Entityes;
using Messages.Bll.Interfaces;
using Messages.Bll.Exceptions;
using Messages.Bll.ModelsBll;
using Messages.Bll.Mappings;

namespace Messages.Bll.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHelper> _passwordHelper;
    private Mapper _mapper;
    private readonly UserService _sut;
    private MapperConfiguration _config;

    public UserServiceTests()
    {
        _config = new MapperConfiguration(
           cfg => cfg.AddProfile(new UserMapperProfileBll())
        );
        _mapper = new Mapper(_config);

        _userRepositoryMock = new Mock<IUserRepository>();
        _passwordHelper = new Mock<IPasswordHelper>();
        _sut = new UserService(_userRepositoryMock.Object, _mapper, _passwordHelper.Object);
    }

    [Fact]
    public async Task AuthenticateUserAsync_NotExistDataUser_EntityNotFoundException() 
    {
        // Arrange
        var message = "password or login does not found";
        // Act
        var exception = await Assert. ThrowsAsync<EntityNotFoundException>(async () =>
            await _sut.AuthenticateUserAsync(new AuthenticateDto() ));
        // Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task AuthenticateUserAsync_DataUser_AuthentificateAndGetUserSuccaessTest()
    {
        // Arrange
        var authenticateDto = new AuthenticateDto() { Nick = "nick", Password = "123456" };
        var userEntity = new UserEntity() { 
            Nick = "nick", Password = "123456", Salt = [ 1, 2 ],  };
        _userRepositoryMock.Setup(t => t.AuthenticateUserAsync(authenticateDto.Nick))
            .ReturnsAsync(userEntity);
        _passwordHelper.Setup(t => t.VerifyPassword(
            authenticateDto.Password, userEntity.Password, userEntity.Salt)).Returns(true);
         _mapper.Map<UserDto>(userEntity);
        // Act
        var result = await _sut.AuthenticateUserAsync(authenticateDto);

        // Assert
        Assert.Equal("nick", result.Nick);
        _userRepositoryMock.Verify(t => t.AuthenticateUserAsync(
            authenticateDto.Nick), Times.Once);
    }
}