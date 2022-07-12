using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.Repository;
using Microsoft.EntityFrameworkCore;
using MakeMyPizza.Data.Seeder;
using Newtonsoft.Json;

namespace MakeMyPizza.Data.Test.Seeder;

public class UserSeederTest
{
    private PizzaOrderManagementDbContext _context;
    public UserSeederTest()
    {
        var _contextOptions = new DbContextOptionsBuilder<PizzaOrderManagementDbContext>()
                .UseInMemoryDatabase("UserSeederTest")
                .ConfigureWarnings(b => b.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
                .Options;

        _context = new PizzaOrderManagementDbContext(_contextOptions);
    }

    [Fact]
    public void SeedUser_Should_Seed_To_UserDbSet()
    {
        // Arrange
        var users = new List<User>();
        users.Add(new User()
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
        });

        var fileName = "UserData.json";
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        using (StreamWriter file = File.CreateText(fileName))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, users);
        }

        // Act
        _context.SeedUser(fileName);

        // Assert
        var result = _context.Users.Find(1);
        Assert.NotNull(result);

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }
}