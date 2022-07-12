using AutoMapper;
using Microsoft.Extensions.Logging;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Dtos;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Domain.Service;
public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    public OrderService(IUnitOfWork unitOfWork,
                        IMapper mapper,
                        ILogger<OrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public List<Order> Get()
    {
        _logger.LogInformation("Get all orders started in OrderService");
        return _unitOfWork.orderRepository.Get(null, null, "Pizzas,NonPizzas,Drinks,Pizzas.Sauces,Pizzas.Toppings");
    }

    public Order Get(int id)
    {
        return _unitOfWork.orderRepository.GetByID(id);
    }

    public CustomerOrderDto PlaceOrder(CustomerOrderDto customerOrder)
    {
        var order = _mapper.Map<Order>(customerOrder.order);
        var user = _mapper.Map<User>(customerOrder.user);
        if (string.IsNullOrEmpty(user.Username))
        {
            user.Username = Guid.NewGuid().ToString();
            _unitOfWork.userRepository.Insert(user);
        }
        else
        {
            user = _unitOfWork.userRepository.Get(u => u.Username == user.Username)?.FirstOrDefault();
        }
        order.UserId = user.Id;
        _unitOfWork.orderRepository.Insert(order);
        _unitOfWork.SaveChanges();

        var orderDto = _mapper.Map<OrderDto>(order);
        var userDto = _mapper.Map<UserDetailsDto>(user);


        return new CustomerOrderDto { order = orderDto, user = userDto };
    }

    public Order PlaceOrder(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        _unitOfWork.orderRepository.Insert(order);
        _unitOfWork.SaveChanges();
        return order;
    }

    // private void CalculatePrice(Order order)
    // {
    //     double price = 0;

    //     price += order.Pizzas.Sum(p => p.Price);
    //     price += order.NonPizzas.Sum(p => p.Price);
    //     price += order.Drinks.Sum(p => p.Price);

    //     order.Price = price;
    // }
}