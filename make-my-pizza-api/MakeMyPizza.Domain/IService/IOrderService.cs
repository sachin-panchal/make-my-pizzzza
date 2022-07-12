using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Dtos;

namespace MakeMyPizza.Domain.IService;
public interface IOrderService
{
    List<Order> Get();
    Order Get(int id);
    Order PlaceOrder(OrderDto order);
    CustomerOrderDto PlaceOrder(CustomerOrderDto order);
}