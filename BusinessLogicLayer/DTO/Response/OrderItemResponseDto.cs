namespace BusinessLogicLayer.DTO.Response;

public class OrderItemResponseDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public ProductResponseDto Product { get; set; }
    public int Count { get; set; }
}
