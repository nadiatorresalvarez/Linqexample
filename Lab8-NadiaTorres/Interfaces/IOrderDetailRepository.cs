using Lab8_NadiaTorres.Models;
namespace Lab8_NadiaTorres.Interfaces;

public interface IOrderDetailRepository
{
    Task<List<object>> GetProductsInOrderAsync(int orderId);
    Task<int> GetTotalQuantityByOrderAsync(int orderId);
    Task<List<string>> GetClientsWhoBoughtProductAsync(int productId);

}