using Lab8_NadiaTorres.Models;
namespace Lab8_NadiaTorres.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersAfterDateAsync(DateTime date);
    Task<List<object>> GetAllOrdersWithDetailsAsync();
    Task<List<string>> GetProductsSoldByClientAsync(int clientId);

}