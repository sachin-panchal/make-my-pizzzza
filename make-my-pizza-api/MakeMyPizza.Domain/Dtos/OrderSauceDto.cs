namespace MakeMyPizza.Domain.Dtos;
public class OrderSauceDto
{
    public int Id { get; set; }
    public int SauceId { get; set; }
    public int OrderPizzaId { get; set; }
    // public int OrderId { get; set; }
    public double Price { get; set; }
}