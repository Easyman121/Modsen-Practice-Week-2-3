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
        var allProds = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.Product.GetAllAsync, cancellationToken);

        NonUniqueException.EnsureUnique(allProds, c => c.Name == productDto.Name,
            $"Product name {productDto.Name} is not unique");

        await DataBase.Product.InsertAsync(_mapper.Map<Product>(productDto), cancellationToken);
    }

    public async Task UpdateProductAsync(int id, ProductRequestDto productDto, CancellationToken cancellationToken)
    {
        CheckFields(productDto, cancellationToken);
        var prodOld =
            await ServiceHelper.CheckAndGetEntityAsync<Product>(DataBase.Product.GetByIdAsync, id, cancellationToken);

        var allProds = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.Product.GetAllAsync, cancellationToken);

        if (prodOld.Name != productDto.Name)
        {
            NonUniqueException.EnsureUnique(allProds, c => c.Name == productDto.Name,
                $"Product name {productDto.Name} is not unique");
        }

        var prodNew = _mapper.Map<Product>(productDto);
        prodNew.Id = id;
        await DataBase.Product.UpdateAsync(prodNew, cancellationToken);
    }

    public async Task<ProductResponseDto> GetProductAsync(int id, CancellationToken cancellationToken)
    {
        var prod = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Product.GetByIdAsync, id, cancellationToken);

        return _mapper.Map<ProductResponseDto>(prod);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var prods = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.Product.GetAllAsync, cancellationToken);

        return _mapper.Map<IEnumerable<ProductResponseDto>>(prods);
    }

    public async Task DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        var prod = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Product.GetByIdAsync, id, cancellationToken);

        await DataBase.Product.DeleteAsync(prod, cancellationToken);
    }

    public static void CheckFields(ProductRequestDto productDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(productDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(productDto.Name);
        RequestDtoException.ThrowIfNullOrWhiteSpace(productDto.Description);
        RequestDtoException.ThrowIfLessThan(productDto.Price, 0);
        RequestDtoException.ThrowIfLessThan(productDto.CategoryId, 0);
    }
}