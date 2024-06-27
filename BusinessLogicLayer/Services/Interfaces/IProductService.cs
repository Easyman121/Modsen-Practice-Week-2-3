using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface IProductService
{
    Task InsertProductAsync(ProductRequestDto productDto, CancellationToken cancellationToken);
    Task UpdateProductAsync(int id, ProductRequestDto productDto, CancellationToken cancellationToken);
    Task DeleteProductAsync(int id, CancellationToken cancellationToken);
    Task<ProductResponseDto> GetProductAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ProductResponseDto>> GetProductsAsync(CancellationToken cancellationToken);
}