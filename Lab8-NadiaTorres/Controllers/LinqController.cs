using Microsoft.AspNetCore.Mvc;
using Lab8_NadiaTorres.Interfaces;

namespace Lab8_NadiaTorres.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LinqController : ControllerBase
{
    private readonly IUnitOfWork _uow;

    public LinqController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    // Ejercicio 1
    [HttpGet("ej1")]
    public async Task<IActionResult> Ej1([FromQuery] string name)
        => Ok(await _uow.Clients.GetByNameContainsAsync(name ?? ""));

    // Ejercicio 2
    [HttpGet("ej2")]
    public async Task<IActionResult> Ej2([FromQuery] decimal price)
        => Ok(await _uow.Products.GetByPriceGreaterThanAsync(price));

    // Ejercicio 3
    [HttpGet("ej3")]
    public async Task<IActionResult> Ej3([FromQuery] int orderId)
        => Ok(await _uow.OrderDetails.GetProductsInOrderAsync(orderId));

    // Ejercicio 4
    [HttpGet("ej4")]
    public async Task<IActionResult> Ej4([FromQuery] int orderId)
        => Ok(new { TotalQuantity = await _uow.OrderDetails.GetTotalQuantityByOrderAsync(orderId) });

    // Ejercicio 5
    [HttpGet("ej5")]
    public async Task<IActionResult> Ej5()
        => Ok(await _uow.Products.GetMostExpensiveAsync());

    // Ejercicio 6
    [HttpGet("ej6")]
    public async Task<IActionResult> Ej6([FromQuery] DateTime date)
        => Ok(await _uow.Orders.GetOrdersAfterDateAsync(date));

    // Ejercicio 7
    [HttpGet("ej7")]
    public async Task<IActionResult> Ej7()
        => Ok(new { AveragePrice = await _uow.Products.GetAveragePriceAsync() });

    // Ejercicio 8
    [HttpGet("ej8")]
    public async Task<IActionResult> Ej8()
        => Ok(await _uow.Products.GetWithoutDescriptionAsync());

    // Ejercicio 9
    [HttpGet("ej9")]
    public async Task<IActionResult> Ej9()
    {
        var (client, count) = await _uow.Clients.GetClientWithMostOrdersAsync();
        return Ok(new { Client = client, Orders = count });
    }

    // Ejercicio 10
    [HttpGet("ej10")]
    public async Task<IActionResult> Ej10()
        => Ok(await _uow.Orders.GetAllOrdersWithDetailsAsync());

    // Ejercicio 11
    [HttpGet("ej11")]
    public async Task<IActionResult> Ej11([FromQuery] int clientId)
        => Ok(await _uow.Orders.GetProductsSoldByClientAsync(clientId));

    // Ejercicio 12
    [HttpGet("ej12")]
    public async Task<IActionResult> Ej12([FromQuery] int productId)
        => Ok(await _uow.OrderDetails.GetClientsWhoBoughtProductAsync(productId));
    
    // Semana 9
    [HttpGet("ej13/clients-orders")]
    public async Task<IActionResult> Ej13()
        => Ok(await _uow.Clients.GetClientsWithOrdersAsync());
    
    [HttpGet("order/{orderId}/details")]
    public async Task<IActionResult> GetOrderDetails([FromRoute] int orderId)
    {
        var result = await _uow.Orders.GetOrderWithDetailsAsync(orderId);
        if (result == null) return NotFound();
        return Ok(result);
    }
    
    [HttpGet("clients/product-count")]
    public async Task<IActionResult> GetClientsProductCount()
        => Ok(await _uow.Clients.GetClientsWithProductCountAsync());
    
    [HttpGet("clients/sales")]
    public async Task<IActionResult> GetSalesByClient()
        => Ok(await _uow.Clients.GetSalesByClientAsync());
}