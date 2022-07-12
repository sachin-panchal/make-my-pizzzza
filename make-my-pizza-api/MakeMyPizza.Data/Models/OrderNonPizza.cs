namespace MakeMyPizza.Data.Models;
public class OrderNonPizza
{
    public int Id { get; set; }
    public int NonPizzaId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
    public Order order { get; set; }
}