using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController(ILogger<CategoryController> logger, ICategoryService category, CancellationToken token)
    : ControllerBase
{
    [HttpGet(Name = "GetCategory")]
    public async Task<CategoryResponseDto> GetCategory(int id) =>
        await category.GetCategoryAsync(id, token);

    [HttpGet(Name = "GetCategories")]
    public async Task<IEnumerable<CategoryResponseDto>> GetCategories() =>
        await category.GetCategoriesAsync(token);

    [HttpPost(Name = "InsertCategory")]
    public async Task<int> Insert(CategoryRequestDto categoryDto) =>
        await category.SetCategoryAsync(categoryDto, token);

    [HttpPost(Name = "UpdateCategory")]
    public async Task Update(int id, CategoryRequestDto categoryDto)
    {
        throw new NotImplementedException();
        return;
    }

    [HttpDelete(Name = "DeleteCategory")]
    public async Task Delete(int id) => throw new NotImplementedException();
}