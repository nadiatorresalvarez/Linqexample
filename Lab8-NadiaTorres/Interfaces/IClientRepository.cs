using Lab8_NadiaTorres.Models.DTOS;

namespace Lab8_NadiaTorres.Interfaces;
using Lab8_NadiaTorres.Models;

public interface IClientRepository
{
    Task<List<Client>> GetByNameContainsAsync(string name);
    Task<(Client? client, int orderCount)> GetClientWithMostOrdersAsync();
    
    // AsNoTracking
    Task<List<ClientOrderDTO>> GetClientsWithOrdersAsync();
    
    // Doble consulta
    Task<List<ClientProductCountDTO>> GetClientsWithProductCountAsync();
    
    // Consulta avanzada
    Task<List<SalesByClientDTO>> GetSalesByClientAsync();

}