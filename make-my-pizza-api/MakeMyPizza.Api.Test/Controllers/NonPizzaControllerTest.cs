using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Api.Test.Controllers;

public class NonPizzaControllerTest
{

    private NonPizzaController _controller;
    private Mock<IBaseService<NonPizza>> _serviceMock;
    public NonPizzaControllerTest()
    {
        var loggerMock = new Mock<ILogger<NonPizzaController>>();
        _serviceMock = new Mock<IBaseService<NonPizza>>();
        _controller = new NonPizzaController(_serviceMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_NonPizzas()
    {
        // Arrange
        var nonPizzas = new List<NonPizza> { new NonPizza { Id = 1, Name = "Choco Lava Cake", Price = 99 } };
        _serviceMock.Setup(s => s.Get()).Returns(nonPizzas);

        // Act
        var result = _controller.Get();
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as List<NonPizza>;
        Assert.NotNull(data);
    }

    [Fact]
    public void Get_Should_Return_NonPizza_By_Id()
    {
        // Arrange
        var nonPizza = new NonPizza { Id = 1, Name = "Choco Lava Cake", Price = 99 };
        _serviceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(nonPizza);

        // Act
        var result = _controller.Get(1);
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as NonPizza;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Post_Should_Add_New_NonPizza()
    {
        // Arrange
        var nonPizza = new NonPizza { Id = 1, Name = "Choco Lava Cake", Price = 99 };
        _serviceMock.Setup(s => s.Insert(It.IsAny<NonPizza>())).Returns(nonPizza);

        // Act
        var result = _controller.Post(nonPizza);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Insert(It.IsAny<NonPizza>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as NonPizza;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Put_Should_Update_NonPizza()
    {
        // Arrange
        var nonPizza = new NonPizza { Id = 1, Name = "Choco Lava Cake", Price = 99 };
        _serviceMock.Setup(s => s.Update(It.IsAny<NonPizza>())).Returns(nonPizza);

        // Act
        var result = _controller.Put(nonPizza);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Update(It.IsAny<NonPizza>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as NonPizza;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Delete_Should_Remove_NonPizza()
    {
        // Arrange
        var nonPizza = new NonPizza { Id = 1, Name = "Choco Lava Cake", Price = 99 };
        _serviceMock.Setup(s => s.Delete(It.IsAny<NonPizza>())).Returns(nonPizza);

        // Act
        var result = _controller.Delete(nonPizza);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<NonPizza>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as NonPizza;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }
}