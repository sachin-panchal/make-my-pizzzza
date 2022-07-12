namespace MakeMyPizza.Domain.Dtos;
public class OrderCheeseDto
{
    public int Id { get; set; }
    public int OrderPizzaId { get; set; }
    public double Price { get; set; }
}