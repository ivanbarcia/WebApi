using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Contracts;
using WebApi.Controllers;
using WebApi.Model;
using Xunit;

namespace WebApi.Tests.Controllers;

public class UserControllerTest
{
    private readonly UserController _controller;
    private readonly Mock<IUserService> _mockUserService = new();
    private readonly Mock<IJwtService> _mockJwtService = new();
    private readonly User _user;

    public UserControllerTest()
    {
        _user = new User() { Id = 1, UserName = "test", Password = "test", Email = "test@test.com" };
        _controller = new UserController(_mockUserService.Object, _mockJwtService.Object);
    }

    [Fact]
    public async Task Get_Should_ReturnsOkResult()
    {
        // Arrange
        _mockUserService.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(_user);

        // Act
        var result = await _controller.Get(_user.UserName);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Get_Should_ReturnNotFound()
    {
        // Arrange

        // Act
        var result = await _controller.Get("test");

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Post_Should_ReturnsOkResult()
    {
        // Arrange
        _mockUserService.Setup(x => x.Add(It.IsAny<User>())).ReturnsAsync(_user);

        // Act
        var result = await _controller.Post(_user);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Post_Should_ReturnBadRequest()
    {
        // Arrange
        var user = new User();
        _controller.ModelState.AddModelError("test", "Is Invalid");

        // Act
        var result = await _controller.Post(user);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Update_Should_ReturnsOkResult()
    {
        // Arrange
        _mockUserService.Setup(x => x.Update(It.IsAny<User>())).ReturnsAsync(_user);

        // Act
        var result = await _controller.Update(_user);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Update_Should_ReturnBadRequest()
    {
        // Arrange
        var user = new User();
        _controller.ModelState.AddModelError("test", "Is Invalid");

        // Act
        var result = await _controller.Update(user);

        // Assert
        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Remove_Should_ReturnsOkResult()
    {
        // Arrange

        _mockUserService.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(_user);

        // Act
        var result = await _controller.Remove(_user.UserName);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task Remove_Should_ReturnNotFound()
    {
        // Arrange

        // Act
        var result = await _controller.Remove("test");

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}