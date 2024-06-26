using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface ICategoryService
{
    Task SetCategoryAsync(CategoryRequestDto? categoryDto, CancellationToken cancellationToken);
    Task<CategoryResponseDto> GetCategoryAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken);
}