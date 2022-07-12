using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Api.Test.Controllers;

public class PizzaControllerTest
{

    private PizzaController _controller;
    private Mock<IBaseService<Pizza>> _serviceMock;
    private Mock<IPizzaService> _pizzaServiceMock;
    public PizzaControllerTest()
    {
        var loggerMock = new Mock<ILogger<PizzaController>>();
        _serviceMock = new Mock<IBaseService<Pizza>>();
        _pizzaServiceMock = new Mock<IPizzaService>();
        _controller = new PizzaController(_serviceMock.Object, _pizzaServiceMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_Pizzas()
    {
        // Arrange
        var Pizzas = new List<PizzaDetailDto> { new PizzaDetailDto { Id = 1, Name = "Peri Peri Paneer", Price = 480, Size = CrustSize.Medium } };
        _pizzaServiceMock.Setup(s => s.GetPizzaList()).Returns(Pizzas);

        // Act
        var result = _controller.Get();
        var okResult = result as OkObjectResult;

        // Assert
        _pizzaServiceMock.Verify(s => s.GetPizzaList(), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as List<PizzaDetailDto>;
        Assert.NotNull(data);
    }

    [Fact]
    public void Get_Should_Return_Pizza_By_Id()
    {
        // Arrange
        var Pizza = new Pizza { Id = 1, Name = "Peri Peri Paneer", Price = 480, Size = CrustSize.Medium };
        _serviceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(Pizza);

        // Act
        var result = _controller.Get(1);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Pizza;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Post_Should_Add_New_Pizza()
    {
        // Arrange
        var Pizza = new Pizza { Id = 1, Name = "Peri Peri Paneer", Price = 480, Size = CrustSize.Medium };
        _serviceMock.Setup(s => s.Insert(It.IsAny<Pizza>())).Returns(Pizza);

        // Act
        var result = _controller.Post(Pizza);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Insert(It.IsAny<Pizza>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Pizza;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Put_Should_Update_Pizza()
    {
        // Arrange
        var Pizza = new Pizza { Id = 1, Name = "Peri Peri Paneer", Price = 480, Size = CrustSize.Medium };
        _serviceMock.Setup(s => s.Update(It.IsAny<Pizza>())).Returns(Pizza);

        // Act
        var result = _controller.Put(Pizza);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Update(It.IsAny<Pizza>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Pizza;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void Delete_Should_Remove_Pizza()
    {
        // Arrange
        var Pizza = new Pizza { Id = 1, Name = "Peri Peri Paneer", Price = 480, Size = CrustSize.Medium };
        _serviceMock.Setup(s => s.Delete(It.IsAny<Pizza>())).Returns(Pizza);

        // Act
        var result = _controller.Delete(Pizza);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.Delete(It.IsAny<Pizza>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Pizza;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }
}