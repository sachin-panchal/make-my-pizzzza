using Microsoft.AspNetCore.Mvc;

using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DrinkController : ControllerBase
{
    private readonly IBaseService<Drink> _service;
    private readonly ILogger<DrinkController> _logger;
    public DrinkController(IBaseService<Drink> service, ILogger<DrinkController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        _logger.LogInformation("Get all drinks");
        var result = _service.Get();
        return this.HandleResponse(result);
    }

    [HttpGet("get/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Get drink by id {id}");
        var result = _service.Get(id);
        return this.HandleResponse(result);
    }

    [HttpPost("add")]
    public IActionResult Post(Drink drink)
    {
        _logger.LogInformation($"Add new drink {drink.Name}");
        var result = _service.Insert(drink);
        return this.HandleResponse(result);
    }

    [HttpPut("update")]
    public IActionResult Put(Drink drink)
    {
        _logger.LogInformation($"Update drink {drink.Name}");
        var result = _service.Update(drink);
        return this.HandleResponse(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Drink drink)
    {
        _logger.LogInformation($"Delete drink {drink.Name}");
        var result = _service.Delete(drink);
        return this.HandleResponse(result);
    }
}
