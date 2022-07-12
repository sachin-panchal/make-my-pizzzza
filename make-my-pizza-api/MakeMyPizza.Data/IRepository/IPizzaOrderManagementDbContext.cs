using MakeMyPizza.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MakeMyPizza.Data.IRepository;

public interface IPizzaOrderManagementDbContext : IDisposable
{
    DbSet<Pizza> Pizzas { get; set; }
    DbSet<PizzaPrice> PizzaPrice { get; set; }
    DbSet<NonPizza> NonPizzas { get; set; }
    DbSet<Drink> Drinks { get; set; }
    DbSet<Sauce> Sauces { get; set; }
    DbSet<Topping> Toppings { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Customer> Customers { get; set; }
    DbSet<User> Users { get; set; }

    int SaveChanges();
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    DatabaseFacade Database { get; }
}