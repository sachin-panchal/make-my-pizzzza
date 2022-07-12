namespace MakeMyPizza.Data.Models;
public class Order
{
    public Order()
    {
        Pizzas = new List<OrderPizza>();
        NonPizzas = new List<OrderNonPizza>();
        Drinks = new List<OrderDrink>();
    }

    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public List<OrderPizza> Pizzas { get; set; }
    public List<OrderNonPizza> NonPizzas { get; set; }
    public List<OrderDrink> Drinks { get; set; }
    public string Status { get; set; }
    public double Price { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}