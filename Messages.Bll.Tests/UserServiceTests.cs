using AutoMapper;
using Moq;
using Messages.Dal.Interfaces;
using Messages.Dal.Entityes;
using Messages.Bll.Interfaces;
using Messages.Bll.Exceptions;
using Messages.Bll.ModelsBll;
using Messages.Bll.Mappings;
using Messages.Dal;
using System;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Messages.Bll.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IPasswordHelper> _passwordHelperMock;
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
        _passwordHelperMock = new Mock<IPasswordHelper>();
        _sut = new UserService(_userRepositoryMock.Object, _mapper, _passwordHelperMock.Object);
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
    public async Task AuthenticateUserAsync_DataUser_AuthentificateAndGetUserSuccaess()
    {
        // Arrange
        var authenticateDto = new AuthenticateDto() { Nick = "nick", Password = "123456" };
        var userEntity = new UserEntity() { 
            Nick = "nick", Password = "123456", Salt = [ 1, 2 ]};
        _userRepositoryMock.Setup(t => t.AuthenticateUserAsync(authenticateDto.Nick))
            .ReturnsAsync(userEntity);
        _passwordHelperMock.Setup(t => t.VerifyPassword(
            authenticateDto.Password, userEntity.Password, userEntity.Salt)).Returns(true);
        // Act
        _mapper.Map<UserDto>(userEntity);
        var result = await _sut.AuthenticateUserAsync(authenticateDto);
        // Assert
        Assert.Equal("nick", result.Nick);
        _userRepositoryMock.Verify(t => t.AuthenticateUserAsync(
            authenticateDto.Nick), Times.Once);
    }
    
    [Fact]
    public async Task CreateUserAsync_NotValidDataUser_UserAlreadyExistsException()
    {
        // Arrange
        var registerDto = new RegisterDto() { Nick = "nick" };
        var message = "this nick already exists";
        _userRepositoryMock.Setup(t => t.GetUserByNickAsync(registerDto.Nick))
            .ReturnsAsync(new UserEntity());
        // Act
        var exception = await Assert.ThrowsAsync<UserAlreadyExistsException>(async () =>
            await _sut.CreateUserAsync(registerDto));
        // Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task CreateUserAsync_DataUser_CreateAndReturnUserSuccaess()
    {
        // Arrange
        UserEntity userEntity = new UserEntity() { Nick = "nick"};
        var registerDto = new RegisterDto() { Nick = "nick" };
        var result = _mapper.Map<UserEntity>(registerDto);
        byte[] saltOut = [1, 2];
        //_passwordHelperMock.Setup(t => t.HashPasword(result.Password, out saltOut))
        //.Returns("password");
        var resuult = result;
        _userRepositoryMock.Setup(t => t.CreateUserAsync(It.IsAny<UserEntity>()))
        .ReturnsAsync(result);
        var newUser = _mapper.Map<UserDto>(result);
        // Act
        var user = await _sut.CreateUserAsync(registerDto);
        // Assert
        Assert.Equal("nick", user.Nick);
    }

    [Fact]
    public async Task UpdateUserAsync_NotValidId_EntityNotFoundException()
    {
        // Arrange
        var message = "user not found";
        var updateUserDto = new UpdateUserDto();
        var guid = Guid.NewGuid();
        // Act
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await _sut.UpdateUserAsync(guid, updateUserDto));
        // Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task UpdateUserAsync_DataForUpdate_FailedToCreateException()
    {
        // Arrange
        var message = "failed to update user";
        var guid = Guid.NewGuid();
        var updateUserDto = new UpdateUserDto();
        var userEntity = new UserEntity();
        _userRepositoryMock.Setup(t => t.GetUserByIdAsync(guid)).
            ReturnsAsync(userEntity);
        // Act
        var exception = await Assert.ThrowsAsync<FailedToCreateException>(async () =>
        await _sut.UpdateUserAsync(guid, updateUserDto));
        // Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task UpdateUserAsync_DataForUpdate_UpdateAndReturnData()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var updateUserDto = new UpdateUserDto();
        var userEntity = new UserEntity() { Name = "name" };
        _userRepositoryMock.Setup(t => t.GetUserByIdAsync(guid)).
            ReturnsAsync(userEntity);
        var newItem = _mapper.Map<UpdateUserEntity>(updateUserDto);
        _userRepositoryMock.Setup(t => t.UpdateUserAsync(
            guid, It.Is<UpdateUserEntity>(n => n.Name == newItem.Name)))
            .ReturnsAsync(userEntity);
        // Act
        var result = await _sut.UpdateUserAsync(guid, updateUserDto);
        // Assert
        Assert.Equal("name", result.Name);
    }

    [Fact]
    public async Task DeleteUserAsync_Guid_EntityNotFoundException()
    {
        // Arrange
        var message = "user not found";
        var guid = Guid.NewGuid();
        //act
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
        await _sut.DeleteUserAsync(guid));
        // Assert
        Assert.Equal(message, exception.Message);
    }

    [Fact]
    public async Task DeleteUserAsync_Guid_DeleteUser()
    {
        // Arrange
        var userEntity = new UserEntity();
        var guid = Guid.NewGuid();
        _userRepositoryMock.Setup(t => t.GetUserByIdAsync(guid)).
        ReturnsAsync(userEntity);
        //act
        await _sut.DeleteUserAsync(guid);
        // Assert
        _userRepositoryMock.Verify(t => t.GetUserByIdAsync(guid), Times.Once);
    }

    //public async Task DeleteUserAsync(Guid id)
    //{
    //    var user = await userRepository.GetUserByIdAsync(id);
    //    if (user is null)
    //        throw new EntityNotFoundException("user not found");
    //    await userRepository.DeleteUserAsync(id);
    //}
}