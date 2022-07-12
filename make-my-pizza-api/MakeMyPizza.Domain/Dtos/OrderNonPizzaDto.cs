namespace MakeMyPizza.Domain.Dtos;
public class OrderNonPizzaDto
{
    public int Id { get; set; }
    public int NonPizzaId { get; set; }
    public int OrderId { get; set; }
    public double Price { get; set; }
}