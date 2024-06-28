using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using Microsoft.AspNetCore.Mvc;

namespace RepresentationLayer.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductController(ILogger<ProductController> logger) : ControllerBase
{
    [HttpGet(Name = "GetProduct")]
    public async Task<ProductResponseDto> GetProduct(int id, CancellationToken token) =>
        throw new NotImplementedException();

    [HttpGet(Name = "GetProducts")]
    public async Task<IEnumerable<ProductResponseDto>> GetProducts(CancellationToken token) =>
        throw new NotImplementedException();

    [HttpPost(Name = "InsertProduct")]
    public async Task<int> Insert(ProductRequestDto productDto, CancellationToken token) =>
        throw new NotImplementedException();

    [HttpPost(Name = "UpdateProduct")]
    public async Task Update(int id, ProductRequestDto productDto, CancellationToken token)
    {
        throw new NotImplementedException();
        return;
    }

    [HttpDelete(Name = "DeleteProduct")]
    public async Task Delete(int id, CancellationToken token) => throw new NotImplementedException();
}