using MakeMyPizza.Data.Models;

namespace MakeMyPizza.Data.IRepository;
public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Order> orderRepository { get; }
    IBaseRepository<Customer> customerRepository { get; }
    IBaseRepository<User> userRepository { get; }
    IBaseRepository<Pizza> pizzaRepoistory { get; }
    IBaseRepository<PizzaPrice> pizzaPriceRepoistory { get; }
    IBaseRepository<NonPizza> nonPizzaRepoistory { get; }
    IBaseRepository<Sauce> sauceRepoistory { get; }
    IBaseRepository<Drink> drinkRepoistory { get; }
    IBaseRepository<Topping> toppingRepository { get; }
    bool SaveChanges();
}