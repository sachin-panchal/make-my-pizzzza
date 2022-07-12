namespace MakeMyPizza.Domain.Dtos;
public class CustomerDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<OrderDto> Orders { get; set; }
}