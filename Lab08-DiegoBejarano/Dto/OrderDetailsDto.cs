namespace Lab08_DiegoBejarano.Dto;

public class OrderDetailsDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<ProductDto2> Products { get; set; }
}