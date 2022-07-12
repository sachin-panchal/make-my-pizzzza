namespace MakeMyPizza.Data.Models;
public class OrderPizza
{
    public OrderPizza()
    {
        Sauces = new List<OrderSauce>();
        Toppings = new List<OrderTopping>();
    }
    public int Id { get; set; }
    public int PizzaId { get; set; }
    public int OrderId { get; set; }
    public CrustSize Size { get; set; }
    public List<OrderTopping> Toppings { get; set; }
    public List<OrderSauce> Sauces { get; set; }
    public OrderCheese Cheese { get; set; }
    public double Price { get; set; }
    public Order Order { get; set; }
}