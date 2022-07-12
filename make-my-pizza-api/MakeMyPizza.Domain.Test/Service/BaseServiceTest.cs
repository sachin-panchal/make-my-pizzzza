using System.Linq.Expressions;
using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Service;
using Microsoft.Extensions.Logging;
using Moq;

namespace MakeMyPizza.Domain.Test.Service;
public class BaseServiceTest
{
    private BaseService<Drink> _baseService;
    private Mock<IBaseRepository<Drink>> _repoMock;
    public BaseServiceTest()
    {
        var loggerMock = new Mock<ILogger<BaseService<Drink>>>();
        _repoMock = new Mock<IBaseRepository<Drink>>();
        _baseService = new BaseService<Drink>(_repoMock.Object, loggerMock.Object);
    }

    [Fact]
    public void Get_Should_Return_All_Drinks()
    {
        // Arrange
        var drinks = new List<Drink> { new Drink { Id = 1, Name = "Pepsi", Price = 70, ImageUrl = "pepsi.jpeg" } };
        _repoMock.Setup(r => r.Get(It.IsAny<Expression<Func<Drink, bool>>>(),
                                  It.IsAny<Func<IQueryable<Drink>, IOrderedQueryable<Drink>>>(),
                                  It.IsAny<string>())
                      ).Returns(drinks);

        // Act
        var result = _baseService.Get();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Get_Should_Return_Drink_For_Id()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Pepsi", Price = 70, ImageUrl = "pepsi.jpeg" };
        _repoMock.Setup(r => r.GetByID(It.IsAny<object>())).Returns(drink);
        
        // Act
        var result = _baseService.Get(1);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void Insert_Should_Add_New_Drink()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Pepsi", Price = 70, ImageUrl = "pepsi.jpeg" };
        _repoMock.Setup(r => r.Insert(It.IsAny<Drink>()));
        
        // Act
        _baseService.Insert(drink);

        // Assert
        _repoMock.Verify(r => r.Insert(It.IsAny<Drink>()), Times.Once);
    }

    [Fact]
    public void Update_Should_Update_Drink()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Pepsi", Price = 70, ImageUrl = "pepsi.jpeg" };
        _repoMock.Setup(r => r.Update(It.IsAny<Drink>()));

        // Act
        _baseService.Update(drink);

        // Assert
        _repoMock.Verify(r => r.Update(It.IsAny<Drink>()), Times.Once);
    }

    [Fact]
    public void Delete_Should_Update_Drink()
    {
        // Arrange
        var drink = new Drink { Id = 1, Name = "Pepsi", Price = 70, ImageUrl = "pepsi.jpeg" };
        _repoMock.Setup(r => r.Delete(It.IsAny<Drink>()));

        // Act
        _baseService.Delete(drink);

        // Assert
        _repoMock.Verify(r => r.Delete(It.IsAny<Drink>()), Times.Once);
    }
}