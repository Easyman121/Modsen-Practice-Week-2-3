using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;

namespace BusinessLogicLayer.Services.Interfaces;

internal interface ICategoryService
{
    Task SetCategoryAsync(CategoryRequestDto categoryDto);
    Task<CategoryResponseDto> GetCategoryAsync(int? id);
    Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync();
}