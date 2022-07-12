namespace MakeMyPizza.Domain.Dtos;
public class OrderDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public virtual List<OrderPizzaDto> Pizzas { get; set; }
    public virtual List<OrderNonPizzaDto> NonPizzas { get; set; }
    public virtual List<OrderDrinkDto> Drinks { get; set; }
    public string Status { get; set; }
    public double Price { get; set; }
}