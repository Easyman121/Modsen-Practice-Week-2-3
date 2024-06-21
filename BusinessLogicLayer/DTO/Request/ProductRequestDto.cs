namespace BusinessLogicLayer.DTO.Request;

public class ProductRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CategoryName { get; set; }
}