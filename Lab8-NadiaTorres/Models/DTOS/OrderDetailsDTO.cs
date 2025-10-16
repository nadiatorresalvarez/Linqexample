namespace Lab8_NadiaTorres.Models.DTOS;

public class OrderDetailsDTO
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ProductDTO> Products { get; set; }
}