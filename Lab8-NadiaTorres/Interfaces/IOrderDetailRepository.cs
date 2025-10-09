using Lab8_NadiaTorres.Models;
using Lab8_NadiaTorres.Models.DTOS;

namespace Lab8_NadiaTorres.Interfaces;

public interface IOrderDetailRepository
{
    Task<List<ProductInOrderDto>> GetProductsInOrderAsync(int orderId);
    Task<int> GetTotalQuantityByOrderAsync(int orderId);
    Task<List<string>> GetClientsWhoBoughtProductAsync(int productId);

}