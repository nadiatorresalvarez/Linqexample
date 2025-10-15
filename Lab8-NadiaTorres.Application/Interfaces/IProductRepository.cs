using Lab8_NadiaTorres.Models;
namespace Lab8_NadiaTorres.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetByPriceGreaterThanAsync(decimal price);
    Task<Product?> GetMostExpensiveAsync();
    Task<List<Product>> GetWithoutDescriptionAsync();
    Task<decimal> GetAveragePriceAsync();
}