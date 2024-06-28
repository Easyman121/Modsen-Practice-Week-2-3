using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

public interface ICategoryService
{
    Task<int> InsertCategoryAsync(CategoryRequestDto categoryDto, CancellationToken cancellationToken);
    Task UpdateCategoryAsync(int id, CategoryRequestDto newCategoryDto, CancellationToken cancellationToken);
    Task DeleteCategoryAsync(int id, CancellationToken cancellationToken);
    Task<CategoryResponseDto> GetCategoryAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ProductResponseDto>> GetProductsAsync(int categoryId, CancellationToken cancellationToken);
}