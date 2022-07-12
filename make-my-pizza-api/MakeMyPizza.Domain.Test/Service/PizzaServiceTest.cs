using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

using AutoMapper;
using Moq;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Service;

public class PizzaServiceTest
{
    [Fact]
    public void GetPizzaList_Should_Get_Pizza_List_With_Medium_Crust_Size_Price()
    {
        // Arrange
        var pizzaRepoMock = new Mock<IBaseRepository<Pizza>>();
        var pizzaPriceRepoMock = new Mock<IBaseRepository<PizzaPrice>>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var mapperMock = new Mock<IMapper>();
        var loggerMock = new Mock<ILogger<PizzaService>>();


        var pizzas = new List<Pizza> { new Pizza { Id = 1, Name = "Pizza name", Size = CrustSize.Medium } };
        var pizzaPrices = new List<PizzaPrice> {
            new PizzaPrice { Id = 1, PizzaId = 1, Size = CrustSize.Small, Price = 350 },
            new PizzaPrice { Id = 1, PizzaId = 1, Size = CrustSize.Medium, Price = 520 },
            new PizzaPrice { Id = 1, PizzaId = 1, Size = CrustSize.Large, Price = 610 }
            };

        var pizzaDetails = new List<PizzaDetailDto> { new PizzaDetailDto { Id = 1, Name = "Pizza name" } };
        pizzaRepoMock.Setup(r => r.Get(It.IsAny<Expression<Func<Pizza, bool>>>(),
                                  It.IsAny<Func<IQueryable<Pizza>, IOrderedQueryable<Pizza>>>(),
                                  It.IsAny<string>())
                      ).Returns(pizzas);
        pizzaPriceRepoMock.Setup(r => r.Get(It.IsAny<Expression<Func<PizzaPrice, bool>>>(),
                                  It.IsAny<Func<IQueryable<PizzaPrice>, IOrderedQueryable<PizzaPrice>>>(),
                                  It.IsAny<string>())
                      ).Returns(pizzaPrices);

        mapperMock.Setup(m => m.Map<List<PizzaDetailDto>>(It.IsAny<List<Pizza>>())).Returns(pizzaDetails);

        var pizzaService = new PizzaService(pizzaRepoMock.Object, pizzaPriceRepoMock.Object, unitOfWorkMock.Object, mapperMock.Object, loggerMock.Object);
        // Act
        var result = pizzaService.GetPizzaList();

        // Assert
        Assert.NotNull(result);
    }
}