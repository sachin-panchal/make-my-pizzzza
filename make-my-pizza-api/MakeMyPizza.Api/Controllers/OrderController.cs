using Microsoft.AspNetCore.Mvc;

using MakeMyPizza.Api.Helpers;
using MakeMyPizza.Domain.Dtos;
using MakeMyPizza.Domain.IService;
using Microsoft.AspNetCore.Authorization;

namespace MakeMyPizza.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrderController> _logger;
    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpGet("GetOrders")]
    public IActionResult Get()
    {
        _logger.LogInformation("Get all orders");
        var result = _orderService.Get();
        return this.HandleResponse(result);
    }

    [HttpPost("GetOrder/{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation($"Get order by id {id}");
        var result = _orderService.Get(id);
        return this.HandleResponse(result);
    }

    [HttpPost("PlaceDirectOrder")]
    public IActionResult PlaceDirectOrder(CustomerOrderDto order)
    {
        _logger.LogInformation("Place direct order");
        var result = _orderService.PlaceOrder(order);
        return this.HandleResponse(result);
    }

    [Authorize]
    [HttpPost("PlaceOrder")]
    public IActionResult PlaceOrder(OrderDto order)
    {
        _logger.LogInformation("Place customer order");
        var result = _orderService.PlaceOrder(order);
        return this.HandleResponse(result);
    }

}