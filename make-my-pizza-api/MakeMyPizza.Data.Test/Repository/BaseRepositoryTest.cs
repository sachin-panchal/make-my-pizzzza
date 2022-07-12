using Microsoft.EntityFrameworkCore;
using Moq;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace MakeMyPizza.Data.Test.Repository;

public class BaseRepositoryTest
{
    private BaseRepository<Order> _baseRepository;
    private PizzaOrderManagementDbContext _context;
    public BaseRepositoryTest()
    {
        var _contextOptions = new DbContextOptionsBuilder<PizzaOrderManagementDbContext>()
                                    .UseInMemoryDatabase("BaseRepositoryTest")
                                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                    .Options;

        _context = new PizzaOrderManagementDbContext(_contextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        var loggerMock = new Mock<ILogger<BaseRepository<Order>>>();
        _baseRepository = new BaseRepository<Order>(_context, loggerMock.Object);
    }

    [Fact]
    public void Test_IPizzaOrderManagementDbContext()
    {
        // Arrange
        var orders = new List<Order>();
        var queryableOrders = orders.AsQueryable();

        var orderDbSetMock = new Mock<DbSet<Order>>();


        orderDbSetMock.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(queryableOrders.Provider);
        orderDbSetMock.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(queryableOrders.Expression);
        orderDbSetMock.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(queryableOrders.ElementType);
        orderDbSetMock.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(queryableOrders.GetEnumerator());


        var contextMock = new Mock<IPizzaOrderManagementDbContext>();
        contextMock.Setup(c => c.Set<Order>()).Returns(orderDbSetMock.Object);
        contextMock.Setup(c => c.SaveChanges()).Returns(1);
        contextMock.SetupGet(x => x.Orders).Returns(orderDbSetMock.Object);
        var loggerMock = new Mock<ILogger<BaseRepository<Order>>>();
        BaseRepository<Order> baseRepository = new BaseRepository<Order>(contextMock.Object, loggerMock.Object);

        var order = new Order()
        {
            Id = 1,
            DeliveryDate = new DateTime(),
            Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, OrderId = 1, Price = 390, Size = CrustSize.Medium } },
            Price = 390,
            Status = "Paid"
        };

        // Act
        baseRepository.Insert(order);
        var result = contextMock.Object.SaveChanges();

        // Assert
        orderDbSetMock.Verify(m => m.Add(It.IsAny<Order>()), Times.Once());
        contextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Fact]
    public void Add_Order_Should_Be_SavedToDatabase()
    {
        // Arrange
        var order = new Order()
        {
            Id = 1,
            DeliveryDate = new DateTime(),
            Pizzas = new List<OrderPizza> { new OrderPizza { Id = 1, OrderId = 1, Price = 390, Size = CrustSize.Medium } },
            NonPizzas = new List<OrderNonPizza> { new OrderNonPizza { Id = 1, OrderId = 1, Price = 90 } },
            Price = 480,
            Status = "Paid"
        };

        // Act
        _baseRepository.Insert(order);
        var result = _context.SaveChanges();

        //Assert
        var orderResult = _baseRepository.GetByID(1);
        Assert.NotNull(orderResult);
    }
}