namespace MakeMyPizza.Domain.Dtos;
public class OrderDrinkDto
{
    public int Id { get; set; }
    public int DrinkId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
}