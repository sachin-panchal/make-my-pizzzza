using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Dtos;

namespace MakeMyPizza.Domain.IService;

public interface IAuthService
{
    Task<UserDto> Register(UserRegisterDto user);
    Task<UserDto> Login(UserLoginDto user);
    Task<bool> UserExists(string username);
}