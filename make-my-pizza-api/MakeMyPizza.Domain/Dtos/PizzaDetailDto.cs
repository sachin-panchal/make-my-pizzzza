using MakeMyPizza.Data.Models;

public class PizzaDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public CrustSize Size { get; set; }
    public double Price { get; set; }
}