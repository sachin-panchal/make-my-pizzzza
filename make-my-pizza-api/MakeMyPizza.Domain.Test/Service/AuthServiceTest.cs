using AutoMapper;
using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Dtos;
using MakeMyPizza.Domain.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Domain.Test.Service;

public class AuthServiceTest
{
    private AuthService _authService;
    public AuthServiceTest()
    {
        var authRepositoryMock = new Mock<IAuthRepository>();
        var mapper = new Mock<IMapper>();
        var logger = new Mock<ILogger<AuthService>>();
        var user = new User { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName" };
        authRepositoryMock.Setup(a => a.Login(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
        authRepositoryMock.Setup(a => a.Register(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(user);
        mapper.Setup(m => m.Map<UserDto>(It.IsAny<User>())).Returns(new UserDto { Id = user.Id, Username = user.Username});
        _authService = new AuthService(authRepositoryMock.Object, mapper.Object, logger.Object);
    }

    [Fact]
    public async Task Login_Should_Login_To_App_Successfully()
    {
        // Act
        var userDto = new UserLoginDto { Username = "username", Password = "password" };
        var result = await _authService.Login(userDto);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Register_Should_Register_To_App_Successfully()
    {

        // Act
        var user = new UserRegisterDto { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName", Password = "password"  };
        var result = await _authService.Register(user);

        // Assert
        Assert.NotNull(result);
    }


}