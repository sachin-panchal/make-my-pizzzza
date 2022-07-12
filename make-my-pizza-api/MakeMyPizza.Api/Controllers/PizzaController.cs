using Microsoft.AspNetCore.Mvc;

using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    private readonly IBaseService<Pizza> _service;
    private readonly IPizzaService _pizzaService;
    private readonly ILogger<PizzaController> _logger;
    public PizzaController(IBaseService<Pizza> service,
                           IPizzaService pizzaService,
                           ILogger<PizzaController> logger)
    {
        _service = service;
        _pizzaService = pizzaService;
        _logger = logger;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        _logger.LogInformation("Get all pizza");
        var result = _pizzaService.GetPizzaList();

        return this.HandleResponse(result);
    }

    [HttpGet("get/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Get pizza by id {id}");
        var result = _service.Get(id);
        return this.HandleResponse(result);
    }

    [HttpPost("add")]
    public IActionResult Post(Pizza pizza)
    {
        _logger.LogInformation($"Add new pizza {pizza.Name}");
        var result = _service.Insert(pizza);
        return this.HandleResponse(result);
    }

    [HttpPut("update")]
    public IActionResult Put(Pizza pizza)
    {
        _logger.LogInformation($"Update pizza {pizza.Name}");
        var result = _service.Update(pizza);
        return this.HandleResponse(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Pizza pizza)
    {
        _logger.LogInformation($"Delete pizza {pizza.Name}");
        var result = _service.Delete(pizza);
        return this.HandleResponse(result);
    }
}
