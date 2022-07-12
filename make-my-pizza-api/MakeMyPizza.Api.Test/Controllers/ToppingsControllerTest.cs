using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Api.Test.Controllers;

public class ToppingsControllerTest
{

    private ToppingsController _controller;
    private Mock<IBaseService<Topping>> _serviceMock;
    public ToppingsControllerTest()
    {
        var loggerMock = new Mock<ILogger<ToppingsController>>();
        _serviceMock = new Mock<IBaseService<Topping>>();
        _controller = new ToppingsController(_serviceMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_Toppings()
    {
        // Arrange
        var toppings = new List<Topping> { new Topping { Id = 1, Name = "Paneer Cubes", Price = 80 } };
        _serviceMock.Setup(s => s.Get()).Returns(toppings);

        // Act
        var result = _controller.Get();
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as List<Topping>;
        Assert.NotNull(data);
    }

    [Fact]
    public void Get_Should_Return_Topping_By_Id()
    {
        // Arrange
        var topping = new Topping { Id = 1, Name = "Paneer Cubes", Price = 80 };
        _serviceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(topping);

        // Act
        var result = _controller.Get(1);
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Topping;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Post_Should_Add_New_Topping()
    {
        // Arrange
        var topping = new Topping { Id = 1, Name = "Paneer Cubes", Price = 80 };
        _serviceMock.Setup(s => s.Insert(It.IsAny<Topping>())).Returns(topping);

        // Act
        var result = _controller.Post(topping);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Insert(It.IsAny<Topping>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Topping;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Put_Should_Update_Topping()
    {
        // Arrange
        var topping = new Topping { Id = 1, Name = "Paneer Cubes", Price = 80 };
        _serviceMock.Setup(s => s.Update(It.IsAny<Topping>())).Returns(topping);

        // Act
        var result = _controller.Put(topping);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Update(It.IsAny<Topping>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Topping;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Delete_Should_Remove_Topping()
    {
        // Arrange
        var topping = new Topping { Id = 1, Name = "Paneer Cubes", Price = 80 };
        _serviceMock.Setup(s => s.Delete(It.IsAny<Topping>())).Returns(topping);

        // Act
        var result = _controller.Delete(topping);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Topping>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Topping;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }
}