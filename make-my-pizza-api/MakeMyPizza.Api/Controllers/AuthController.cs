using Microsoft.AspNetCore.Mvc;

using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Domain.Dtos;
using MakeMyPizza.Domain.IService;
using MakeMyPizza.Api.Utils;

namespace MakeMyPizza.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;
    public AuthController(IAuthService authService, 
                         IConfiguration configuration,
                         ILogger<AuthController> logger)
    {
        _authService = authService;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        _logger.LogInformation("Register new user");
        var user = await _authService.Register(request);
        var token = user.CreateToken(_configuration);

        return this.HandleResponse(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto request)
    {
        _logger.LogInformation("Login to app");
        var user = await _authService.Login(request);
        var token = user.CreateToken(_configuration);
        
        return this.HandleResponse(token);
    }
}