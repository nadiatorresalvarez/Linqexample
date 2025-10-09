using Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;
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
}