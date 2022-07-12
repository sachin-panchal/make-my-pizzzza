using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Authentication;

using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;
using Moq;
using Microsoft.Extensions.Logging;

namespace MakeMyPizza.Data.Test.Repository;

public class AuthRepositoryTest
{
    private PizzaOrderManagementDbContext _context;
    private AuthRepository _authRepository;
    public AuthRepositoryTest()
    {
        var _contextOptions = new DbContextOptionsBuilder<PizzaOrderManagementDbContext>()
                                    .UseInMemoryDatabase("AuthRepositoryTest")
                                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                    .Options;

        _context = new PizzaOrderManagementDbContext(_contextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        var loggerMock = new Mock<ILogger<AuthRepository>>();
        _authRepository = new AuthRepository(_context, loggerMock.Object);
    }

    [Fact]
    public async Task Login_Should_Register_And_Successfully_Login()
    {
        // Arrange
        var user = new User()
        {
            Id = 1,
            Username = "username",
            FirstName = "firstName",
            LastName = "lastName",
            Phone = "9876654320",
            Email = "abc@xyz.com",
            Address = "Address 1",
            City = "City",
            Pincode = "123 456"
        };

        var password = "superSecretPassword";

        // Act        
        var registeredUser = await _authRepository.Register(user, password);
        var result = await _authRepository.Login(user.Username, password);

        // Assert

        Assert.NotNull(registeredUser);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Login_Should_Throw_AuthenticationException()
    {
        // Arrange
        var user = new User()
        {
            Id = 1,
            Username = "username",
            FirstName = "firstName",
            LastName = "lastName",
            Phone = "9876654320",
            Email = "abc@xyz.com",
            Address = "Address 1",
            City = "City",
            Pincode = "123 456"
        };

        var password = "superSecretPassword";
        var incorrectPassword = "guess1";
        // Act        
        var registeredUser = await _authRepository.Register(user, password);
        Func<Task> loginAction = () => _authRepository.Login(user.Username, incorrectPassword);

        // Assert
        Assert.NotNull(registeredUser);
        var exception = await Assert.ThrowsAsync<AuthenticationException>(loginAction);
        Assert.Equal("Invalid credentials", exception.Message);
    }

}