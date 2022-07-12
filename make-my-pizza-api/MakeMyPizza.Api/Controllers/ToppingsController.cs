using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Mvc;

namespace MakeMyPizza.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ToppingsController :  ControllerBase
{
    private readonly IBaseService<Topping> _service;
    private readonly ILogger<ToppingsController> _logger;
    public ToppingsController(IBaseService<Topping> service,
                             ILogger<ToppingsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        _logger.LogInformation("Get all toppings");
        var result = _service.Get();
        return this.HandleResponse(result);
    }

    [HttpGet("get/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Get topping by id {id}");
        var result = _service.Get(id);
        return this.HandleResponse(result);
    }

    [HttpPost("add")]
    public IActionResult Post(Topping topping)
    {
        _logger.LogInformation($"Add new topping {topping.Name}");
        var result = _service.Insert(topping);
        return this.HandleResponse(result);
    }

    [HttpPut("update")]
    public IActionResult Put(Topping topping)
    {
        _logger.LogInformation($"Update topping {topping.Name}");
        var result = _service.Update(topping);
        return this.HandleResponse(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Topping topping)
    {
        _logger.LogInformation($"Delete topping {topping.Name}");
        var result = _service.Delete(topping);
        return this.HandleResponse(result);
    }
}
