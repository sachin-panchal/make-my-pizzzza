namespace MakeMyPizza.Data.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string? Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Pincode { get; set; }
    public Order Order { get; set; }
}