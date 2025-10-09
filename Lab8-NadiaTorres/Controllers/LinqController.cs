using Microsoft.AspNetCore.Mvc;
using Lab8_NadiaTorres.Interfaces;

namespace Lab8_NadiaTorres.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinqController : ControllerBase
{
    private readonly IClientRepository _clientRepo;
    private readonly IProductRepository _productRepo;
    private readonly IOrderRepository _orderRepo;
    private readonly IOrderDetailRepository _orderDetailRepo;

    public LinqController(IClientRepository clientRepo, IProductRepository productRepo, IOrderRepository orderRepo, IOrderDetailRepository orderDetailRepo)
    {
        _clientRepo = clientRepo;
        _productRepo = productRepo;
        _orderRepo = orderRepo;
        _orderDetailRepo = orderDetailRepo;
    }

    // Ejercicio 1
    [HttpGet("ej1")]
    public async Task<IActionResult> Ej1([FromQuery] string name)
        => Ok(await _clientRepo.GetByNameContainsAsync(name ?? ""));

    // Ejercicio 2
    [HttpGet("ej2")]
    public async Task<IActionResult> Ej2([FromQuery] decimal price)
        => Ok(await _productRepo.GetByPriceGreaterThanAsync(price));

    // Ejercicio 3
    [HttpGet("ej3")]
    public async Task<IActionResult> Ej3([FromQuery] int orderId)
        => Ok(await _orderDetailRepo.GetProductsInOrderAsync(orderId));

    // Ejercicio 4
    [HttpGet("ej4")]
    public async Task<IActionResult> Ej4([FromQuery] int orderId)
        => Ok(new { TotalQuantity = await _orderDetailRepo.GetTotalQuantityByOrderAsync(orderId) });

    // Ejercicio 5
    [HttpGet("ej5")]
    public async Task<IActionResult> Ej5()
        => Ok(await _productRepo.GetMostExpensiveAsync());

    // Ejercicio 6
    [HttpGet("ej6")]
    public async Task<IActionResult> Ej6([FromQuery] DateTime date)
        => Ok(await _orderRepo.GetOrdersAfterDateAsync(date));

    // Ejercicio 7
    [HttpGet("ej7")]
    public async Task<IActionResult> Ej7()
        => Ok(new { AveragePrice = await _productRepo.GetAveragePriceAsync() });

    // Ejercicio 8
    [HttpGet("ej8")]
    public async Task<IActionResult> Ej8()
        => Ok(await _productRepo.GetWithoutDescriptionAsync());

    // Ejercicio 9
    [HttpGet("ej9")]
    public async Task<IActionResult> Ej9()
    {
        var (client, count) = await _clientRepo.GetClientWithMostOrdersAsync();
        return Ok(new { Client = client, Orders = count });
    }

    // Ejercicio 10
    [HttpGet("ej10")]
    public async Task<IActionResult> Ej10()
        => Ok(await _orderRepo.GetAllOrdersWithDetailsAsync());

    // Ejercicio 11
    [HttpGet("ej11")]
    public async Task<IActionResult> Ej11([FromQuery] int clientId)
        => Ok(await _orderRepo.GetProductsSoldByClientAsync(clientId));

    // Ejercicio 12
    [HttpGet("ej12")]
    public async Task<IActionResult> Ej12([FromQuery] int productId)
        => Ok(await _orderDetailRepo.GetClientsWhoBoughtProductAsync(productId));
}