namespace MakeMyPizza.Data.Models;
public class OrderSauce
{
    public int Id { get; set; }
    public int SauceId { get; set; }
    public int OrderPizzaId { get; set; }
    public double Price { get; set; }
    public OrderPizza OrderPizza { get; set; }
}