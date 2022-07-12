namespace MakeMyPizza.Data.Models;
public class OrderTopping
{
    public int Id { get; set; }
    public int ToppingId { get; set; }
    // public int PizzaId { get; set; }
    public int OrderPizzaId { get; set; }
    public double Price { get; set; }
    public OrderPizza OrderPizza { get; set; }
}