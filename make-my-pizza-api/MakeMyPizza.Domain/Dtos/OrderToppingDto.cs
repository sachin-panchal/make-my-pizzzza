namespace MakeMyPizza.Domain.Dtos;
public class OrderToppingDto
{
    public int Id { get; set; }
    public int ToppingId { get; set; }
    public int OrderPizzaId { get; set; }
    // public int OrderId { get; set; }
    public double Price { get; set; }
}