using Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;
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
}