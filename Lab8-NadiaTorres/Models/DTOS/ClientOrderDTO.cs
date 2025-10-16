namespace Lab8_NadiaTorres.Models.DTOS;

public class ClientOrderDTO
{
    public String ClientName { get; set; }
    public List<OrderDTO> Orders { get; set; }
}