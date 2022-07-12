using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Api.Test.Controllers;

public class SauceControllerTest
{

    private SauceController _controller;
    private Mock<IBaseService<Sauce>> _serviceMock;
    public SauceControllerTest()
    {
        var loggerMock = new Mock<ILogger<SauceController>>();
        _serviceMock = new Mock<IBaseService<Sauce>>();
        _controller = new SauceController(_serviceMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_Sauces()
    {
        // Arrange
        var sauces = new List<Sauce> { new Sauce { Id = 1, Name = "Marinara", Price = 60 } };
        _serviceMock.Setup(s => s.Get()).Returns(sauces);

        // Act
        var result = _controller.Get();
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as List<Sauce>;
        Assert.NotNull(data);
    }

    [Fact]
    public void Get_Should_Return_Sauce_By_Id()
    {
        // Arrange
        var sauce = new Sauce { Id = 1, Name = "Marinara", Price = 60 };
        _serviceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(sauce);

        // Act
        var result = _controller.Get(1);
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Sauce;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Post_Should_Add_New_Sauce()
    {
        // Arrange
        var sauce = new Sauce { Id = 1, Name = "Marinara", Price = 60 };
        _serviceMock.Setup(s => s.Insert(It.IsAny<Sauce>())).Returns(sauce);

        // Act
        var result = _controller.Post(sauce);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Insert(It.IsAny<Sauce>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Sauce;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Put_Should_Update_Sauce()
    {
        // Arrange
        var sauce = new Sauce { Id = 1, Name = "Marinara", Price = 60 };
        _serviceMock.Setup(s => s.Update(It.IsAny<Sauce>())).Returns(sauce);

        // Act
        var result = _controller.Put(sauce);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Update(It.IsAny<Sauce>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Sauce;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Delete_Should_Remove_Sauce()
    {
        // Arrange
        var sauce = new Sauce { Id = 1, Name = "Marinara", Price = 60 };
        _serviceMock.Setup(s => s.Delete(It.IsAny<Sauce>())).Returns(sauce);

        // Act
        var result = _controller.Delete(sauce);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Sauce>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Sauce;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }
}