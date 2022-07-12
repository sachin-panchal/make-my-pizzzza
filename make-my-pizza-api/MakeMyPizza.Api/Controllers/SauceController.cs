using Microsoft.AspNetCore.Mvc;

using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Data.Models;
using MakeMyPizza.Domain.IService;

namespace MakeMyPizza.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SauceController : ControllerBase
{
    private readonly IBaseService<Sauce> _service;
    private readonly ILogger<SauceController> _logger;
    public SauceController(IBaseService<Sauce> service,
                           ILogger<SauceController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet("get")]
    public IActionResult Get()
    {
        _logger.LogInformation("Get all sauces");
        var result = _service.Get();
        return this.HandleResponse(result);
    }

    [HttpGet("get/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Get sauce by id {id}");
        var result = _service.Get(id);
        return this.HandleResponse(result);
    }

    [HttpPost("add")]
    public IActionResult Post(Sauce sauce)
    {
        _logger.LogInformation($"Add new sauce {sauce.Name}");
        var result = _service.Insert(sauce);
        return this.HandleResponse(result);
    }

    [HttpPut("update")]
    public IActionResult Put(Sauce sauce)
    {
        _logger.LogInformation($"Update sauce {sauce.Name}");
        var result = _service.Update(sauce);
        return this.HandleResponse(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Sauce sauce)
    {
        _logger.LogInformation($"Delete sauce {sauce.Name}");
        var result = _service.Delete(sauce);
        return this.HandleResponse(result);
    }
}
