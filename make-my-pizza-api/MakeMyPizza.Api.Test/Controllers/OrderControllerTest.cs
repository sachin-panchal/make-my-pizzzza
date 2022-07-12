using MakeMyPizza.Api.Controllers;
using MakeMyPizza.Api.Models;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Dtos;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Api.Test.Controllers;

public class OrderControllerTest
{

    private OrderController _controller;
    private Mock<IOrderService> _serviceMock;
    public OrderControllerTest()
    {
        var loggerMock = new Mock<ILogger<OrderController>>();
        _serviceMock = new Mock<IOrderService>();
        _controller = new OrderController(_serviceMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_Orders()
    {
        // Arrange
        var orders = new List<Order> { new Order { Id = 1, Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, PizzaId = 1, OrderId = 1, Price = 460 } }, Price = 460 } };
        _serviceMock.Setup(s => s.Get()).Returns(orders);

        // Act
        var result = _controller.Get();
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as List<Order>;
        Assert.NotNull(data);
    }

    [Fact]
    public void Get_Should_Return_Order_By_Id()
    {
        // Arrange
        var order = new Order { Id = 1, Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, PizzaId = 1, OrderId = 1, Price = 460 } }, Price = 460 };
        _serviceMock.Setup(s => s.Get(It.IsAny<int>())).Returns(order);

        // Act
        var result = _controller.Get(1);
        var okResult = result as OkObjectResult;
        
        // Assert
        _serviceMock.Verify(s => s.Get(It.IsAny<int>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Order;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void PlaceOrder_Should_Add_New_Order()
    {
        // Arrange
        var orderDto = new OrderDto { Id = 1, Pizzas = new List<OrderPizzaDto> { new OrderPizzaDto { Id = 1, PizzaId = 1, OrderId = 1, Price = 460 } }, Price = 460 };
        var order = new Order { Id = 1, Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, PizzaId = 1, OrderId = 1, Price = 460 } }, Price = 460 };
        _serviceMock.Setup(s => s.PlaceOrder(It.IsAny<OrderDto>())).Returns(order);

        // Act
        var result = _controller.PlaceOrder(orderDto);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.PlaceOrder(It.IsAny<OrderDto>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as Order;
        Assert.NotNull(data);
        Assert.Equal(1, data.Id);
    }

    [Fact]
    public void PlaceOrder_Should_Add_Direct_Order()
    {
        // Arrange
        var customerOrderDto = new CustomerOrderDto
        {
            order = new OrderDto { Id = 1, Pizzas = new List<OrderPizzaDto> { new OrderPizzaDto { Id = 1, OrderId = 1, Price = 360 } }, Price = 360 },
            user = new UserDetailsDto { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName" }
        };
        var order = new Order { Id = 1, Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, PizzaId = 1, OrderId = 1, Price = 460 } }, Price = 460 };
        var user = new User { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName" };

        _serviceMock.Setup(s => s.PlaceOrder(It.IsAny<CustomerOrderDto>())).Returns(customerOrderDto);

        // Act
        var result = _controller.PlaceDirectOrder(customerOrderDto);
        var okResult = result as OkObjectResult;

        // Assert
        _serviceMock.Verify(s => s.PlaceOrder(It.IsAny<CustomerOrderDto>()), Times.Once);

        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<ClientResponse>(okResult.Value);

        var response = okResult.Value as ClientResponse;
        var data = response.data as CustomerOrderDto;
        Assert.NotNull(data);
    }
}