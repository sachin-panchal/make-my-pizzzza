using Moq;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;
using Microsoft.Extensions.Logging;

namespace MakeMyPizza.Data.Test.Repository;

public class UnitOfWorkTest
{
    private UnitOfWork _unitOfWork;
    Mock<IPizzaOrderManagementDbContext> _contextMock;
    public UnitOfWorkTest()
    {
        _contextMock = new Mock<IPizzaOrderManagementDbContext>();

        var orderRepositoryMock = new Mock<IBaseRepository<Order>>();
        var customerRepositoryMock = new Mock<IBaseRepository<Customer>>();
        var userRepositoryMock = new Mock<IBaseRepository<User>>();
        var pizzaRepoistoryMock = new Mock<IBaseRepository<Pizza>>();
        var pizzaPriceRepoistoryMock = new Mock<IBaseRepository<PizzaPrice>>();
        var nonPizzaRepoistoryMock = new Mock<IBaseRepository<NonPizza>>();
        var sauceRepoistoryMock = new Mock<IBaseRepository<Sauce>>();
        var drinkRepoistoryMock = new Mock<IBaseRepository<Drink>>();
        var toppingRepositoryMock = new Mock<IBaseRepository<Topping>>();
        var loggerMock = new Mock<ILogger<UnitOfWork>>();

        _unitOfWork = new UnitOfWork(_contextMock.Object,
                                    orderRepositoryMock.Object,
                                    customerRepositoryMock.Object,
                                    userRepositoryMock.Object,
                                    pizzaRepoistoryMock.Object,
                                    pizzaPriceRepoistoryMock.Object,
                                    nonPizzaRepoistoryMock.Object,
                                    sauceRepoistoryMock.Object,
                                    drinkRepoistoryMock.Object,
                                    toppingRepositoryMock.Object,
                                    loggerMock.Object
        );
    }

    [Fact]
    public void SaveChanges_Test()
    {
        _unitOfWork.SaveChanges();

        _contextMock.Verify(x => x.SaveChanges(), Times.Once);
    }
}