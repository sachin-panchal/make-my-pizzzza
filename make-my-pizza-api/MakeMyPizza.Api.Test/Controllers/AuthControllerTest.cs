using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Domain.Dtos;
using MakeMyPizza.Domain.IService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Api.Test.Controllers;

public class AuthControllerTest
{
    private AuthController _authController;
    private Mock<IAuthService> _authServiceMock;
    public AuthControllerTest()
    {
        // Arrange
        _authServiceMock = new Mock<IAuthService>();
        var configurationMock = new Mock<IConfiguration>();
        var loggerMock = new Mock<ILogger<AuthController>>();

        var appSettingTokenSectionMock = new Mock<IConfigurationSection>();
        appSettingTokenSectionMock.Setup(a => a.Path).Returns("AppSettings");
        appSettingTokenSectionMock.Setup(a => a.Key).Returns("Token");
        appSettingTokenSectionMock.Setup(a => a.Value).Returns("my top secret key");

        configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Returns(appSettingTokenSectionMock.Object);

        _authController = new AuthController(_authServiceMock.Object, configurationMock.Object, loggerMock.Object);
    }

    [Fact]
    public async Task Register_Should_Register_User_And_Create_Auth_Token()
    {
        // Arrange
        var userDto = new UserDto { Id = 1, Username = "username" };
        _authServiceMock.Setup(a => a.Register(It.IsAny<UserRegisterDto>())).ReturnsAsync(userDto);

        // Act
        var user = new UserRegisterDto { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName", Password = "password" };
        var result = await _authController.Register(user);
        var okResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;
        // Assert
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as string;
        Assert.NotNull(data);
    }

    [Fact]
    public async Task Login_Should_Login_User_And_Create_Auth_Token()
    {
        // Arrange
        var userDto = new UserDto { Id = 1, Username = "username" };
        _authServiceMock.Setup(a => a.Login(It.IsAny<UserLoginDto>())).ReturnsAsync(userDto);

        // Act
        var user = new UserLoginDto { Username = "username", Password = "password" };
        var result = await _authController.Login(user);
        var okResult = result as Microsoft.AspNetCore.Mvc.OkObjectResult;
        // Assert
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as string;
        Assert.NotNull(data);
    }
}