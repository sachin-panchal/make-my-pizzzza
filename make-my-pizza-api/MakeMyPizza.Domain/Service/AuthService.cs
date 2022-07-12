using AutoMapper;

using MakeMyPizza.Data.IRepository;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.Dtos;
using MakeMyPizza.Domain.IService;
using Microsoft.Extensions.Logging;

namespace MakeMyPizza.Domain.Service;
public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;
    public AuthService(IAuthRepository authRepository, 
                       IMapper mapper,
                       ILogger<AuthService> logger)
    {
        _authRepository = authRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UserDto> Login(UserLoginDto userDto)
    {
        _logger.LogInformation("Login started in AuthService");
        var user = await _authRepository.Login(userDto.Username, userDto.Password);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> Register(UserRegisterDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        var result = await _authRepository.Register(user, userDto.Password);
        return _mapper.Map<UserDto>(result);
    }

    public async Task<bool> UserExists(string username)
    {
        return await _authRepository.UserExists(username);
    }

}