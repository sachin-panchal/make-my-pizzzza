using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Api.Test.Controllers;

public class PizzaPriceControllerTest
{

    private PizzaPriceController _controller;
    private Mock<IBaseService<PizzaPrice>> _serviceMock;
    public PizzaPriceControllerTest()
    {
        var loggerMock = new Mock<ILogger<PizzaPriceController>>();
        _serviceMock = new Mock<IBaseService<PizzaPrice>>();
        _controller = new PizzaPriceController(_serviceMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_PizzaPrices()
    {
        // Arrange
        var pizzaPrices = new List<PizzaPrice> { new PizzaPrice { Id = 1, PizzaId = 1, Price = 480, Size = CrustSize.Medium } };
        _serviceMock.Setup(s => s.Get()).Returns(pizzaPrices);

        // Act
        var result = _controller.Get();
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Get(), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as List<PizzaPrice>;
        Assert.NotNull(data);
    }

    [Fact]
    public void Get_Should_Return_PizzaPrice_By_Id()
    {
        // Arrange
        var pizzaPrice = new PizzaPrice { Id = 1, PizzaId = 1, Price = 480, Size = CrustSize.Medium };
        _serviceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(pizzaPrice);

        // Act
        var result = _controller.Get(1);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as PizzaPrice;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Post_Should_Add_New_PizzaPrice()
    {
        // Arrange
        var pizzaPrice = new PizzaPrice { Id = 1, PizzaId = 1, Price = 480, Size = CrustSize.Medium };
        _serviceMock.Setup(s => s.Insert(It.IsAny<PizzaPrice>())).Returns(pizzaPrice);

        // Act
        var result = _controller.Post(pizzaPrice);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Insert(It.IsAny<PizzaPrice>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as PizzaPrice;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Put_Should_Update_PizzaPrice()
    {
        // Arrange
        var pizzaPrice = new PizzaPrice { Id = 1, PizzaId = 1, Price = 480, Size = CrustSize.Medium };
        _serviceMock.Setup(s => s.Update(It.IsAny<PizzaPrice>())).Returns(pizzaPrice);

        // Act
        var result = _controller.Put(pizzaPrice);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Update(It.IsAny<PizzaPrice>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as PizzaPrice;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Delete_Should_Remove_PizzaPrice()
    {
        // Arrange
        var pizzaPrice = new PizzaPrice { Id = 1, PizzaId = 1, Price = 480, Size = CrustSize.Medium };
        _serviceMock.Setup(s => s.Delete(It.IsAny<PizzaPrice>())).Returns(pizzaPrice);

        // Act
        var result = _controller.Delete(pizzaPrice);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<PizzaPrice>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as PizzaPrice;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }
}