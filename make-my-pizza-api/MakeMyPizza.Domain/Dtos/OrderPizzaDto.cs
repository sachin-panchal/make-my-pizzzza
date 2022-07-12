using MakeMyPizza.Data.Models;

namespace MakeMyPizza.Domain.Dtos;
public class OrderPizzaDto
{
    public int Id { get; set; }
    public int PizzaId { get; set; }
    public int OrderId { get; set; }
    public CrustSize Size { get; set; }
    public List<OrderToppingDto> Toppings { get; set; }
    public List<OrderSauceDto> Sauces { get; set; }
    public OrderCheeseDto Cheese { get; set; }
    public double Price { get; set; }
}