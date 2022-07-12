namespace MakeMyPizza.Domain.Dtos;
public class UserDetailsDto
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string? Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Pincode { get; set; }
}