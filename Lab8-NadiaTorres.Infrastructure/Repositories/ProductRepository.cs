using Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;
using Microsoft.EntityFrameworkCore;
namespace Lab8_NadiaTorres.Repositories;

public class ProductRepository : IProductRepository
{
        private readonly dbContextnLab8 _ctx;
    public ProductRepository(dbContextnLab8 ctx) => _ctx = ctx;

    public async Task<List<Product>> GetByPriceGreaterThanAsync(decimal price) =>
        await _ctx.Products.Where(p => p.Price > price).ToListAsync();

    public async Task<Product?> GetMostExpensiveAsync() =>
        await _ctx.Products.OrderByDescending(p => p.Price).FirstOrDefaultAsync();

    public async Task<List<Product>> GetWithoutDescriptionAsync() =>
        await _ctx.Products.Where(p => string.IsNullOrEmpty(p.Description)).ToListAsync();

    public async Task<decimal> GetAveragePriceAsync() =>
        await _ctx.Products.AverageAsync(p => p.Price);


}