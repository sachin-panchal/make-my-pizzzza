namespace MakeMyPizza.Data.Models;
public class OrderDrink
{
    public int Id { get; set; }
    public int DrinkId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public Order order { get; set; }
}