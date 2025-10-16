using Lab8_NadiaTorres.Models;
using Lab8_NadiaTorres.Models.DTOS;

namespace Lab8_NadiaTorres.Interfaces;

public interface IOrderRepository
{
    Task<List<Order>> GetOrdersAfterDateAsync(DateTime date);
    Task<List<object>> GetAllOrdersWithDetailsAsync();
    Task<List<string>> GetProductsSoldByClientAsync(int clientId);
    
    //Include
    Task<OrderDetailsDTO?> GetOrderWithDetailsAsync(int orderId);

}