namespace MakeMyPizza.Domain.Dtos;
public class CustomerOrderDto
{
    public UserDetailsDto user { get; set; }
    public OrderDto order { get; set; }
}