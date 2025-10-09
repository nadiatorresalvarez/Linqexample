using Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab8_NadiaTorres.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly dbContextnLab8 _ctx;
    public OrderDetailRepository(dbContextnLab8 ctx) => _ctx = ctx;

    public async Task<List<object>> GetProductsInOrderAsync(int orderId) =>
        await _ctx.Orderdetails
            .Where(od => od.OrderId == orderId)
            .Select(od => new { ProductName = od.Product.Name, od.Quantity })
            .ToListAsync<object>();

    public async Task<int> GetTotalQuantityByOrderAsync(int orderId) =>
        await _ctx.Orderdetails.Where(od => od.OrderId == orderId).SumAsync(od => od.Quantity);

    public async Task<List<string>> GetClientsWhoBoughtProductAsync(int productId)
    {
        var clients = await (from od in _ctx.Orderdetails
                             where od.ProductId == productId
                             join o in _ctx.Orders on od.OrderId equals o.OrderId
                             join c in _ctx.Clients on o.ClientId equals c.ClientId
                             select c.Name)
                            .Distinct()
                            .ToListAsync();
        return clients;
    }
}