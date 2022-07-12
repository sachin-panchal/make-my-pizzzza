namespace MakeMyPizza.Data.Models;

public class PizzaPrice
{
    public int Id { get; set; }
    public int PizzaId { get; set; }
    public CrustSize Size { get; set; }
    public double Price { get; set; }
}