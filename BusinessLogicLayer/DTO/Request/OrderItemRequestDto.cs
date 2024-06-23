using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.DTO.Request

public class OrderItemRequestDto
{
    public ProductRequestDto Product { get; set; }
    public int Count { get; set; }
}

