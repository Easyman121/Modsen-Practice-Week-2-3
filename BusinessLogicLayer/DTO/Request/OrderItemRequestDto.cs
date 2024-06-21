using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.DTO.Request
{
    public class OrderItemRequestDto
    {
        public ProductResponseDto Product { get; set; }
        public int Count { get; set; }

    }
}
