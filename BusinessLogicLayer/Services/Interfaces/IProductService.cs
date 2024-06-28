using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IProductService
{
    Task<int> InsertProductAsync(ProductRequestDto productDto, CancellationToken cancellationToken);
    Task UpdateProductAsync(int id, ProductRequestDto productDto, CancellationToken cancellationToken);
    Task DeleteProductAsync(int id, CancellationToken cancellationToken);
    Task<ProductResponseDto> GetProductAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<ProductResponseDto>> GetProductsAsync(CancellationToken cancellationToken);
    Task<CategoryResponseDto> GetCategoryAsync(int productId, CancellationToken cancellationToken);
}