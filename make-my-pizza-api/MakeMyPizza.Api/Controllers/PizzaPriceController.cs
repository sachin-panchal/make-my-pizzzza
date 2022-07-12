using Microsoft.AspNetCore.Mvc;

using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaPriceController : ControllerBase
{
    private readonly IBaseService<PizzaPrice> _service;
    private readonly ILogger<PizzaPriceController> _logger;
    public PizzaPriceController(IBaseService<PizzaPrice> service, ILogger<PizzaPriceController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        _logger.LogInformation("Get all pizza prices");
        var result = _service.Get();
        return this.HandleResponse(result);
    }

    [HttpGet("get/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Get pizza price by id {id}");
        var result = _service.Get(id);
        return this.HandleResponse(result);
    }

    [HttpGet("getByPizzaId/{id}")]
    public IActionResult GetByPizzaId(int id)
    {
        _logger.LogInformation($"Get pizza price by pizza id {id}");
        var result = _service.Get(p => p.PizzaId == id);
        return this.HandleResponse(result);
    }

    [HttpPost("add")]
    public IActionResult Post(PizzaPrice pizzaPrice)
    {
        _logger.LogInformation($"Add new pizza price for pizza id {pizzaPrice.PizzaId}");
        var result = _service.Insert(pizzaPrice);
        return this.HandleResponse(result);
    }

    [HttpPut("update")]
    public IActionResult Put(PizzaPrice pizzaPrice)
    {
        _logger.LogInformation($"Update pizza price for pizza id {pizzaPrice.PizzaId}");
        var result = _service.Update(pizzaPrice);
        return this.HandleResponse(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(PizzaPrice pizzaPrice)
    {
        _logger.LogInformation($"Delete pizza price for pizza id {pizzaPrice.PizzaId}");
        var result = _service.Delete(pizzaPrice);
        return this.HandleResponse(result);
    }
}
