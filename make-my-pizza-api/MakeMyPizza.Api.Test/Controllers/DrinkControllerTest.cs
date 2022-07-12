using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;

using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Api.Test.Controllers;

public class DrinkControllerTest
{
    private DrinkController _controller;
    private Mock<IBaseService<Drink>> _serviceMock;
    public DrinkControllerTest()
    {
        var loggerMock = new Mock<ILogger<DrinkController>>();
        _serviceMock = new Mock<IBaseService<Drink>>();
        _controller = new DrinkController(_serviceMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_Drinks()
    {
        // Arrange
        var drinks = new List<Drink> { new Drink { Id = 1, Name = "Pepsi", Price = 75 } };
        _serviceMock.Setup(s => s.Get()).Returns(drinks);

        // Act
        var result = _controller.Get();
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as List<Drink>;
        Assert.NotNull(data);
    }

    [Fact]
    public void Get_Should_Return_Drink_By_Id()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Pepsi", Price = 75 };
        _serviceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(drink);

        // Act
        var result = _controller.Get(1);
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Drink;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Post_Should_Add_New_Drink()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Pepsi", Price = 75 };
        _serviceMock.Setup(s => s.Insert(It.IsAny<Drink>())).Returns(drink);

        // Act
        var result = _controller.Post(drink);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Insert(It.IsAny<Drink>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Drink;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Put_Should_Update_Drink()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Pepsi", Price = 75 };
        _serviceMock.Setup(s => s.Update(It.IsAny<Drink>())).Returns(drink);

        // Act
        var result = _controller.Put(drink);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Update(It.IsAny<Drink>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Drink;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Delete_Should_Remove_Drink()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Pepsi", Price = 75 };
        _serviceMock.Setup(s => s.Delete(It.IsAny<Drink>())).Returns(drink);

        // Act
        var result = _controller.Delete(drink);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Drink>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Drink;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }
}