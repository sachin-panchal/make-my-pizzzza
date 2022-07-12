using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MakeMyPizza.Data.Repository;

public class PizzaOrderManagementDbContext : DbContext, IPizzaOrderManagementDbContext
{
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<PizzaPrice> PizzaPrice { get; set; }
    public DbSet<NonPizza> NonPizzas { get; set; }
    public DbSet<Drink> Drinks { get; set; }
    public DbSet<Sauce> Sauces { get; set; }
    public DbSet<Topping> Toppings { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
    
    public PizzaOrderManagementDbContext(DbContextOptions<PizzaOrderManagementDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
                    .Property(p => p.Id)
                    .ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>()
                    .Property(p => p.Id)
                    .ValueGeneratedOnAdd();

        // modelBuilder.Entity<OrderPizza>()
        //             .HasOne(m => m.order)
        //             .WithMany(mi => mi.Pizzas)
        //             .HasForeignKey(mi => mi.OrderId); 
    }

}