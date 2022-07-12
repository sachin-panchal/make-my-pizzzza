namespace MakeMyPizza.Data.Models;

public class Customer
{
    public Customer()
    {
        Orders = new List<Order>();
    }
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<Order> Orders { get; set; }
}