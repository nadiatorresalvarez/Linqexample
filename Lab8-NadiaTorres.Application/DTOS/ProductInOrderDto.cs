namespace Lab8_NadiaTorres.Models.DTOS;

public class ProductInOrderDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal? Price { get; set; } // opcional, si quieres mostrar precio

}