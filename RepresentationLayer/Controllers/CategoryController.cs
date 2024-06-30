using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService)
    : ControllerBase
{
    [HttpGet(Name = "GetCategory")]
    public async Task<CategoryResponseDto?> GetCategoryAsync(int id, CancellationToken token) =>
        await categoryService.GetCategoryAsync(id, token);

    [HttpGet(Name = "GetCategories")]
    public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync(CancellationToken token) =>
        await categoryService.GetCategoriesAsync(token);

    [HttpGet(Name = "GetCategoryProducts")]
    public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(int id, CancellationToken token) =>
        await categoryService.GetProductsAsync(id, token);

    [HttpPost(Name = "InsertCategory")]
    public async Task<int> InsertAsync(CategoryRequestDto categoryDto, CancellationToken token) =>
        await categoryService.InsertCategoryAsync(categoryDto, token);

    [HttpPost(Name = "UpdateCategory")]
    public async Task UpdateAsync(int id, CategoryRequestDto categoryDto, CancellationToken token) =>
        await categoryService.UpdateCategoryAsync(id, categoryDto, token);

    [HttpDelete(Name = "DeleteCategory")]
    public async Task DeleteAsync(int id, CancellationToken token) =>
        await categoryService.DeleteCategoryAsync(id, token);
}