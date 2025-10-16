using Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;
using Lab8_NadiaTorres.Models.DTOS;
using Microsoft.EntityFrameworkCore;

namespace Lab8_NadiaTorres.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly dbContextnLab8 _ctx;
    public ClientRepository(dbContextnLab8 ctx) => _ctx = ctx;

    public async Task<List<Client>> GetByNameContainsAsync(string name) =>
        await _ctx.Clients.Where(c => c.Name.Contains(name)).ToListAsync();

    public async Task<(Client? client, int orderCount)> GetClientWithMostOrdersAsync()
    {
        var q = await _ctx.Orders
            .GroupBy(o => o.ClientId)
            .Select(g => new { ClientId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .FirstOrDefaultAsync();

        if (q == null) return (null, 0);
        var client = await _ctx.Clients.FindAsync(q.ClientId);
        return (client, q.Count);
    }
    
    //AsNoTracking
    public async Task<List<ClientOrderDTO>> GetClientsWithOrdersAsync()
    {
        var clientOrders = await _ctx.Clients
            .AsNoTracking()
            .Select(client => new ClientOrderDTO
            {
                ClientName = client.Name,
                Orders = client.Orders
                    .Select(order => new OrderDTO
                    {
                        OrderId = order.OrderId,
                        OrderDate = order.OrderDate
                    })
                    .ToList()
            })
            .ToListAsync();

        return clientOrders;
    }
    
    // Doble consulta
    public async Task<List<ClientProductCountDTO>> GetClientsWithProductCountAsync()
    {
        return await _ctx.Clients
            .AsNoTracking() // Mejora el rendimiento para consultas de solo lectura
            .Select(client => new ClientProductCountDTO
            {
                ClientName = client.Name,
                // Suma las cantidades de todos los detalles en todas las órdenes del cliente
                TotalProducts = client.Orders.SelectMany(order => order.Orderdetails).Sum(detail => detail.Quantity)
            })
            .ToListAsync();
    }
    
    // Consulta avanzada
    public async Task<List<SalesByClientDTO>> GetSalesByClientAsync()
    {
        var sales = await _ctx.Orders
            .Include(order => order.Orderdetails)
            .ThenInclude(orderDetail => orderDetail.Product)
            .AsNoTracking()
            .GroupBy(order => order.ClientId)
            .Select(group => new SalesByClientDTO
            {
                ClientName = _ctx.Clients.FirstOrDefault(c => c.ClientId == group.Key).Name,
                TotalSales = group.Sum(order => order.Orderdetails.Sum(detail => detail.Quantity * detail.Product.Price))
            })
            .OrderByDescending(s => s.TotalSales)
            .ToListAsync();

        return sales;
    }
}