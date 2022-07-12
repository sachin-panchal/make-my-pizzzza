using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

using AutoMapper;
using Moq;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Service;
using MakeMyPizza.Domain.Dtos;

namespace MakeMyPizza.Domain.Test.Service;
public class OrderServiceTest
{
    private Mock<IBaseRepository<Order>> _orderRepoMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private Mock<IMapper> _mapperMock;
    private OrderService _orderService;
    public OrderServiceTest()
    {
        _orderRepoMock = new Mock<IBaseRepository<Order>>();

        _unitOfWorkMock = new Mock<IUnitOfWork>();
        var loggerMock = new Mock<ILogger<OrderService>>();

        _mapperMock = new Mock<IMapper>();
        _unitOfWorkMock.Setup(u => u.orderRepository).Returns(_orderRepoMock.Object);
        
        _orderService = new OrderService(_unitOfWorkMock.Object, _mapperMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get()
    {
        // Arrange
        var orders = new List<Order>
        {
             new Order
             {
                 Id = 1,
                 UserId = 1,
                 Pizzas = new List<OrderPizza>
                 {
                     new OrderPizza
                     {
                         Id = 1,
                         OrderId = 1,
                         Price = 360
                    }
                 },
                 Price = 360,
                 OrderDate = DateTime.Now
            }
        };

        _orderRepoMock.Setup(r => r.Get(It.IsAny<Expression<Func<Order, bool>>>(),
                                  It.IsAny<Func<IQueryable<Order>, IOrderedQueryable<Order>>>(),
                                  It.IsAny<string>())
                      ).Returns(orders);

        // Act
        var result = _orderService.Get();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Get_Should_Return_Order_By_Id()
    {
        // Arrange
        var order = new Order { Id = 1, UserId = 1, Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, OrderId = 1, Price = 360 } }, Price = 360 };
        _orderRepoMock.Setup(r => r.GetByID(It.IsAny<object>())).Returns(order);

        // Act
        var result = _orderService.Get(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void PlaceOrder_Should_Add_Direct_Order()
    {
        // Arrange
        var userRepoMock = new Mock<IBaseRepository<User>>();
        var order = new Order { Id = 1, UserId = 1, Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, OrderId = 1, Price = 360 } }, Price = 360 };
        var user = new User { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName" };

        var customerOrderDto = new CustomerOrderDto
        {
            order = new OrderDto { Id = 1, Pizzas = new List<OrderPizzaDto> { new OrderPizzaDto { Id = 1, OrderId = 1, Price = 360 } }, Price = 360 },
            user = new UserDetailsDto { Id = 1, Username = "username", FirstName = "firstName", LastName = "lastName" }
        };

        _orderRepoMock.Setup(r => r.Insert(It.IsAny<Order>()));
        userRepoMock.Setup(r => r.Get(It.IsAny<Expression<Func<User, bool>>>(),
                                          It.IsAny<Func<IQueryable<User>, IOrderedQueryable<User>>>(),
                                          It.IsAny<string>())
                              ).Returns(new List<User> { user });
        _mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderDto>())).Returns(order);
        _mapperMock.Setup(m => m.Map<User>(It.IsAny<UserDetailsDto>())).Returns(user);
        _mapperMock.Setup(m => m.Map<OrderDto>(It.IsAny<Order>())).Returns(customerOrderDto.order);
        _mapperMock.Setup(m => m.Map<UserDetailsDto>(It.IsAny<User>())).Returns(customerOrderDto.user);
        
        _unitOfWorkMock.Setup(u => u.userRepository).Returns(userRepoMock.Object);
        _unitOfWorkMock.Setup(u => u.SaveChanges()).Returns(true);
        // Act

        var result = _orderService.PlaceOrder(customerOrderDto);

        // Assert
        Assert.NotNull(result);
        _unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Once);
        _orderRepoMock.Verify(o => o.Insert(It.IsAny<Order>()), Times.Once);
    }

    [Fact]
    public void PlaceOrder_Should_Add_Registered_User_Order()
    {
        // Arrange
        var order = new Order { Id = 1, UserId = 1, Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, OrderId = 1, Price = 360 } }, Price = 360 };
        var orderDto = new OrderDto { Id = 1, Pizzas = new List<OrderPizzaDto> { new OrderPizzaDto { Id = 1, OrderId = 1, Price = 360 } }, Price = 360 };

        _orderRepoMock.Setup(r => r.Insert(It.IsAny<Order>()));
        _mapperMock.Setup(m => m.Map<Order>(It.IsAny<OrderDto>())).Returns(order);
        _mapperMock.Setup(m => m.Map<OrderDto>(It.IsAny<Order>())).Returns(orderDto);

        _unitOfWorkMock.Setup(u => u.SaveChanges()).Returns(true);

        // Act
        var result = _orderService.PlaceOrder(orderDto);

        // Assert
        Assert.NotNull(result);
        _unitOfWorkMock.Verify(u => u.SaveChanges(), Times.Once);
        _orderRepoMock.Verify(o => o.Insert(It.IsAny<Order>()), Times.Once);
    }
}