using FluentAssertions;
using Moq;
using WebApi.Context;
using WebApi.Contracts;
using WebApi.Model;
using WebApi.Services;
using Xunit;

namespace WebApi.Tests.Services;

public class UserServiceTest
{
    private readonly UserService _service;
    private readonly Mock<IDataContext> _dataContext = new();

    public UserServiceTest()
    {
        _service = new UserService(_dataContext.Object);
    }

    [Fact]
    public async Task CheckPasswordAsync_Should_True()
    {
        // Arrange
        _dataContext.Setup(x => x.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

        var result = await _service.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>());

        result.Should().BeTrue();
    }

    [Fact]
    public async Task CheckPasswordAsync_Should_False()
    {
        // Arrange
        _dataContext.Setup(x => x.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

        var result = await _service.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>());

        result.Should().BeFalse();
    }

    [Fact]
    public async Task Add_Should_ReturnUser()
    {
        // Arrange
        var user = new User();
        _dataContext.Setup(x => x.AddUser(user));
        _dataContext.Setup(x => x.SaveChanges());

        var result = await _service.Add(user);

        result.Should().Be(user);
        _dataContext.Verify(x => x.AddUser(user), Times.Once);
        _dataContext.Verify(x => x.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task Update_Should_ReturnUser()
    {
        // Arrange
        var user = new User();
        _dataContext.Setup(x => x.UpdateUser(user));
        _dataContext.Setup(x => x.SaveChanges());

        // Act
        var result = await _service.Update(user);

        // Assert
        result.Should().Be(user);
        _dataContext.Verify(x => x.UpdateUser(user), Times.Once);
        _dataContext.Verify(x => x.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task Remove_Should_OK()
    {
        // Arrange
        var userId = 1;
        _dataContext.Setup(x => x.RemoveUser(userId));
        _dataContext.Setup(x => x.SaveChanges());

        // Act
        await _service.Remove(userId);

        // Assert
        _dataContext.Verify(x => x.RemoveUser(userId), Times.Once);
        _dataContext.Verify(x => x.SaveChanges(), Times.Once);
    }
}