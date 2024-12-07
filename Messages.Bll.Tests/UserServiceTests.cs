
using Moq;
using Messages.Dal.Interfaces;

namespace Messages.Bll.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    
    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
    }

    [Fact]
    public void Test1()
    {

    }
}