using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

internal class ProductService : IProductService
{
    private IUnitOfWork DataBase { get; set; }
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task InsertProductAsync(ProductRequestDto productDto, CancellationToken cancellationToken)
    {
        CheckFields(productDto, cancellationToken);
        var allProds = await CheckAndGetProductsAsync(cancellationToken);
        if (allProds.Exists(c => c.Name == productDto.Name))
        {
            throw new NonUniqueException("The product name is not unique");
        }

        await DataBase.Product.InsertAsync(_mapper.Map<Product>(productDto), cancellationToken);
    }

    public async Task UpdateProductAsync(int id, ProductRequestDto productDto, CancellationToken cancellationToken)
    {
        CheckFields(productDto, cancellationToken);
        var prodOld = await CheckAndGetProductAsync(id, cancellationToken);

        var allProds = await CheckAndGetProductsAsync(cancellationToken);

        if (prodOld.Name != productDto.Name)
        {
            if (allProds.Exists(c => c.Name == productDto.Name))
            {
                throw new NonUniqueException("The product name is not unique");
            }
        }

        var prodNew = _mapper.Map<Product>(productDto);
        prodNew.Id = id;
        DataBase.Product.UpdateAsync(prodNew, cancellationToken);
    }

    public async Task<ProductResponseDto> GetProductAsync(int id, CancellationToken cancellationToken)
    {
        var prod = await CheckAndGetProductAsync(id, cancellationToken);

        return _mapper.Map<ProductResponseDto>(prod);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var prods = await CheckAndGetProductsAsync(cancellationToken);

        return _mapper.Map<IEnumerable<ProductResponseDto>>(prods);
    }

    public async Task DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        var prod = await CheckAndGetProductAsync(id, cancellationToken);

        DataBase.Product.DeleteAsync(prod, cancellationToken);
    }

    public static void CheckFields(ProductRequestDto productDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(productDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(productDto.Name);
        RequestDtoException.ThrowIfNullOrWhiteSpace(productDto.Description);
    }

    private async Task<Product> CheckAndGetProductAsync(int id, CancellationToken cancellationToken)
    {
        RequestDtoException.ThrowIfLessThan(id, 0);
        var prod = await DataBase.Product.GetByIdAsync(id, cancellationToken);
        if (prod == null)
        {
            throw new RequestDtoException("No product entries found");
        }

        return prod;
    }

    private async Task<List<Product>> CheckAndGetProductsAsync(CancellationToken cancellationToken)
    {
        var prods = await DataBase.Product.GetAllAsync(cancellationToken);

        if (prods.Count == 0)
        {
            throw new RequestDtoException("The list is empty");
        }

        return prods;
    }
}