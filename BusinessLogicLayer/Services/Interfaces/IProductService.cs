using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IProductService
{
    Task SetProductAsync(ProductRequestDto productDto);
    Task<ProductResponseDto> GetProductAsync(int? id);
    Task<IEnumerable<ProductResponseDto>> GetProductsAsync();
}