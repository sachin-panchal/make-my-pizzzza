using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Api.Test.Controllers;

public class UserControllerTest
{

    private UserController _controller;
    private Mock<IBaseService<User>> _serviceMock;
    public UserControllerTest()
    {
        var loggerMock = new Mock<ILogger<UserController>>();
        _serviceMock = new Mock<IBaseService<User>>();
        _controller = new UserController(_serviceMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_Users()
    {
        // Arrange
        var users = new List<User> { new User { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName", Email = "abc@xyz.com", Phone = "9876543210" } };
        _serviceMock.Setup(s => s.Get()).Returns(users);

        // Act
        var result = _controller.Get();
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as List<User>;
        Assert.NotNull(data);
    }

    [Fact]
    public void Get_Should_Return_User_By_Id()
    {
        // Arrange
        var user = new User { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName", Email = "abc@xyz.com", Phone = "9876543210" };
        _serviceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(user);

        // Act
        var result = _controller.Get(1);
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as User;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Post_Should_Add_New_User()
    {
        // Arrange
        var user = new User { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName", Email = "abc@xyz.com", Phone = "9876543210" };
        _serviceMock.Setup(s => s.Insert(It.IsAny<User>())).Returns(user);

        // Act
        var result = _controller.Post(user);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Insert(It.IsAny<User>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as User;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Put_Should_Update_User()
    {
        // Arrange
        var user = new User { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName", Email = "abc@xyz.com", Phone = "9876543210" };
        _serviceMock.Setup(s => s.Update(It.IsAny<User>())).Returns(user);

        // Act
        var result = _controller.Put(user);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Update(It.IsAny<User>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as User;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Delete_Should_Remove_User()
    {
        // Arrange
        var user = new User { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName", Email = "abc@xyz.com", Phone = "9876543210" };
        _serviceMock.Setup(s => s.Delete(It.IsAny<User>())).Returns(user);

        // Act
        var result = _controller.Delete(user);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<User>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as User;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }
}