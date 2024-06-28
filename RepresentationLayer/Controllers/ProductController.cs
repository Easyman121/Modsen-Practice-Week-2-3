using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductController(ILogger<ProductController> logger, IProductService productService) : ControllerBase
{
    [HttpGet(Name = "GetProduct")]
    public async Task<ProductResponseDto> GetProductAsync(int id, CancellationToken token) =>
        await productService.GetProductAsync(id, token);

    [HttpGet(Name = "GetProducts")]
    public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(CancellationToken token) =>
        await productService.GetProductsAsync(token);

    [HttpGet(Name = "GetCategory")]
    public async Task<CategoryResponseDto> GetCategoryAsync(int id, CancellationToken token) =>
        await productService.GetCategoryAsync(id, token);

    [HttpPost(Name = "InsertProduct")]
    public async Task<int> InsertAsync(ProductRequestDto productDto, CancellationToken token) =>
        await productService.InsertProductAsync(productDto, token);

    [HttpPost(Name = "UpdateProduct")]
    public async Task UpdateAsync(int id, ProductRequestDto productDto, CancellationToken token) =>
        await productService.UpdateProductAsync(id, productDto, token);

    [HttpDelete(Name = "DeleteProduct")]
    public async Task DeleteAsync(int id, CancellationToken token) =>
        await productService.DeleteProductAsync(id, token);
}