using Microsoft.AspNetCore.Mvc;

using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class NonPizzaController : ControllerBase
{
    private readonly IBaseService<NonPizza> _service;
    private readonly ILogger<NonPizzaController> _logger;
    public NonPizzaController(IBaseService<NonPizza> service, ILogger<NonPizzaController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        _logger.LogInformation("Get all non pizza items");
        var result = _service.Get();
        return this.HandleResponse(result);
    }

    [HttpGet("get/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Get non pizza item by id {id}");
        var result = _service.Get(id);
        return this.HandleResponse(result);
    }

    [HttpGet("get/cheese")]
    public IActionResult GetCheese()
    {
        _logger.LogInformation("Get cheese details");
        var result = _service.Get(i => i.Name == "Cheese")?.FirstOrDefault();

        if(result is null)
            throw new Exception("Someone moved cheese");

        return this.HandleResponse(result);
    }

    [HttpPost("add")]
    public IActionResult Post(NonPizza nonPizza)
    {
        _logger.LogInformation($"Add new non pizza item {nonPizza.Name}");
        var result = _service.Insert(nonPizza);
        return this.HandleResponse(result);
    }

    [HttpPut("update")]
    public IActionResult Put(NonPizza nonPizza)
    {
        _logger.LogInformation($"Update non pizza item {nonPizza.Name}");
        var result = _service.Update(nonPizza);
        return this.HandleResponse(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(NonPizza nonPizza)
    {
        _logger.LogInformation($"Delete non pizza item {nonPizza.Name}");
        var result = _service.Delete(nonPizza);
        return this.HandleResponse(result);
    }
}
