using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Domain.IService;
using MakeMyPizza.Data.Models;

namespace MakeMyPizza.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IBaseService<Data.Models.User> _userService;
    private readonly ILogger<UserController> _logger;
    public UserController(IBaseService<Data.Models.User> userService,
                          ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }


    [HttpGet("GetUser")]
    public IActionResult Get()
    {
        _logger.LogInformation("Get all users");
        var users = _userService.Get();
        return this.HandleResponse(users);
    }

    [HttpGet("GetUser/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Get user by id {id}");
        var user = _userService.Get(id);
        return this.HandleResponse(user);
    }

    [HttpPost("add")]
    public IActionResult Post(User user)
    {
        _logger.LogInformation($"Add new user {user.Username}");
        var result = _userService.Insert(user);
        return this.HandleResponse(result);
    }

    [HttpPut("update")]
    public IActionResult Put(User user)
    {
        _logger.LogInformation($"Update user {user.Username}");
        var result = _userService.Update(user);
        return this.HandleResponse(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(User user)
    {
        _logger.LogInformation($"Delete user {user.Username}");
        var result = _userService.Delete(user);
        return this.HandleResponse(result);
    }
}