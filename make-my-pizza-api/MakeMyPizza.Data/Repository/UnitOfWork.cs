using MakeMyPizza.Data.Models;
using MakeMyPizza.Data.IRepository;
using Microsoft.Extensions.Logging;

namespace MakeMyPizza.Data.Repository;

public class UnitOfWork : IUnitOfWork
{
    protected virtual IPizzaOrderManagementDbContext Context { get; set; }
    public IBaseRepository<Order> orderRepository { get { return _orderRepository; } }
    public IBaseRepository<Customer> customerRepository { get { return _customerRepository; } }
    public IBaseRepository<User> userRepository { get { return _userRepository; } }
    public IBaseRepository<Pizza> pizzaRepoistory { get { return _pizzaRepoistory; } }
    public IBaseRepository<PizzaPrice> pizzaPriceRepoistory { get { return _pizzaPriceRepoistory; } }
    public IBaseRepository<NonPizza> nonPizzaRepoistory { get { return _nonPizzaRepoistory; } }
    public IBaseRepository<Sauce> sauceRepoistory { get { return _sauceRepoistory; } }
    public IBaseRepository<Drink> drinkRepoistory { get { return _drinkRepoistory; } }
    public IBaseRepository<Topping> toppingRepository { get { return _toppingRepository; } }

    #region private
    private IBaseRepository<Order> _orderRepository;
    private IBaseRepository<Customer> _customerRepository;
    private IBaseRepository<User> _userRepository;
    private IBaseRepository<Pizza> _pizzaRepoistory;
    private IBaseRepository<PizzaPrice> _pizzaPriceRepoistory;
    private IBaseRepository<NonPizza> _nonPizzaRepoistory;
    private IBaseRepository<Sauce> _sauceRepoistory;
    private IBaseRepository<Drink> _drinkRepoistory;
    private IBaseRepository<Topping> _toppingRepository;

    private readonly ILogger<UnitOfWork> _logger;
    #endregion

    public UnitOfWork(IPizzaOrderManagementDbContext context,
                      IBaseRepository<Order> orderRepository,
                      IBaseRepository<Customer> customerRepository,
                      IBaseRepository<User> userRepository,
                      IBaseRepository<Pizza> pizzaRepoistory,
                      IBaseRepository<PizzaPrice> pizzaPriceRepoistory,
                      IBaseRepository<NonPizza> nonPizzaRepoistory,
                      IBaseRepository<Sauce> sauceRepoistory,
                      IBaseRepository<Drink> drinkRepoistory,
                      IBaseRepository<Topping> toppingRepository,
                      ILogger<UnitOfWork> logger
                      )
    {
        Context = context;
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _userRepository = userRepository;
        _pizzaRepoistory = pizzaRepoistory;
        _pizzaPriceRepoistory = pizzaPriceRepoistory;
        _nonPizzaRepoistory = nonPizzaRepoistory;
        _sauceRepoistory = sauceRepoistory;
        _drinkRepoistory = drinkRepoistory;
        _toppingRepository = toppingRepository;
        _logger = logger;
    }

    public bool SaveChanges()
    {
        bool returnValue = true;

        using (var dbContextTransaction = Context.Database.BeginTransaction())
        {
            try
            {
                Context.SaveChanges();
                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                returnValue = false;
                dbContextTransaction.Rollback();
            }
        }

        return returnValue;
    }


    #region Dispose  
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion

}