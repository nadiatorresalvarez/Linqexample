using Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;
using Lab8_NadiaTorres.Models.DTOS;
using Microsoft.EntityFrameworkCore;

namespace Lab8_NadiaTorres.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly dbContextnLab8 _ctx;
    public OrderRepository(dbContextnLab8 ctx) => _ctx = ctx;

    public async Task<List<Order>> GetOrdersAfterDateAsync(DateTime date) =>
        await _ctx.Orders.Where(o => o.OrderDate > date).ToListAsync();

    public async Task<List<object>> GetAllOrdersWithDetailsAsync()
    {
        return await _ctx.Orders
            .Select(o => new {
                o.OrderId,
                o.ClientId,
                Details = o.Orderdetails.Select(d => new { ProductName = d.Product.Name, d.Quantity })
            })
            .AsNoTracking()
            .ToListAsync<object>();
    }

    public async Task<List<string>> GetProductsSoldByClientAsync(int clientId)
    {
        var products = await (from o in _ctx.Orders
                              where o.ClientId == clientId
                              join od in _ctx.Orderdetails on o.OrderId equals od.OrderId
                              join p in _ctx.Products on od.ProductId equals p.ProductId
                              select p.Name)
                             .Distinct()
                             .ToListAsync();
        return products;
    }
    
    //Include y ThenInclude
    public async Task<OrderDetailsDTO?> GetOrderWithDetailsAsync(int orderId)
    {
        var ordersWithDetails = await _ctx.Orders
            .Include(order => order.Orderdetails)                 // Cargar detalles de la orden
            .ThenInclude(orderDetail => orderDetail.Product)      // Cargar productos asociados
            .AsNoTracking()                                       // Mejor rendimiento en lectura
            .Where(order => order.OrderId == orderId)             // Filtrar por OrderId
            .Select(order => new OrderDetailsDTO
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Products = order.Orderdetails
                    .Select(od => new ProductDTO
                    {
                        ProductName = od.Product.Name,
                        Quantity = od.Quantity,
                        Price = od.Product.Price
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();

        return ordersWithDetails;
    }
}