using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IProductService
{
    Task SetProductAsync(ProductRequestDto productDto, CancellationToken cancellationToken);
    Task<ProductResponseDto> GetProductAsync(int? id, CancellationToken cancellationToken);
    Task<IEnumerable<ProductResponseDto>> GetProductsAsync(CancellationToken cancellationToken);
}