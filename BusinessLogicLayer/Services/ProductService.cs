using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class ProductService(IUnitOfWork uow) : IProductService
{
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task<int> InsertProductAsync(ProductRequestDto productDto, CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(productDto, cancellationToken);
        var allProds = await ServiceHelper.GetEntitiesAsync(uow.Product.GetAllAsync, cancellationToken);
        if (allProds.Count > 0)
        {
            NonUniqueException.EnsureUnique(allProds, c => c.Name == productDto.Name,
                $"Product name {productDto.Name} is not unique");
        }

        var prod = _mapper.Map<Product>(productDto);
        prod.Category =
            await ServiceHelper.CheckAndGetEntityAsync(uow.Category.GetByIdAsync, prod.CategoryId, cancellationToken);
        return await uow.Product.InsertAsync(prod, cancellationToken);
    }

    public async Task UpdateProductAsync(int id, ProductRequestDto productDto, CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(productDto, cancellationToken);
        var prodOld =
            await ServiceHelper.CheckAndGetEntityAsync<Product>(uow.Product.GetByIdAsync, id, cancellationToken);

        var allProds = await ServiceHelper.CheckAndGetEntitiesAsync(uow.Product.GetAllAsync, cancellationToken);

        if (prodOld.Name != productDto.Name)
        {
            NonUniqueException.EnsureUnique(allProds, c => c.Name == productDto.Name,
                $"Product name {productDto.Name} is not unique");
        }

        var prodNew = _mapper.Map<Product>(productDto);
        prodNew.Id = id;
        prodNew.Category =
            prodNew.CategoryId == prodOld.CategoryId
                ? await ServiceHelper.CheckAndGetEntityAsync(uow.Category.GetByIdAsync, prodNew.CategoryId,
                    cancellationToken)
                : prodOld.Category;
        await uow.Product.UpdateAsync(prodNew, cancellationToken);
    }

    public async Task<ProductResponseDto> GetProductAsync(int id, CancellationToken cancellationToken)
    {
        var prod = await ServiceHelper.CheckAndGetEntityAsync(uow.Product.GetByIdAsync, id, cancellationToken);

        return _mapper.Map<ProductResponseDto>(prod);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(CancellationToken cancellationToken)
    {
        var prods = await ServiceHelper.GetEntitiesAsync(uow.Product.GetAllAsync, cancellationToken);

        return _mapper.Map<IEnumerable<ProductResponseDto>>(prods);
    }

    public async Task DeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        var prod = await ServiceHelper.CheckAndGetEntityAsync(uow.Product.GetByIdAsync, id, cancellationToken);

        await uow.Product.DeleteAsync(prod, cancellationToken);
    }

    public async Task<CategoryResponseDto> GetCategoryAsync(int productId, CancellationToken cancellationToken)
    {
        var prod = await ServiceHelper.CheckAndGetEntityAsync(uow.Product.GetByIdAsync, productId,
            cancellationToken);
        return _mapper.Map<CategoryResponseDto>(prod.Category);
    }

    public static void CheckFieldsAndToken(ProductRequestDto productDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(productDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(productDto.Name);
        RequestDtoException.ThrowIfNullOrWhiteSpace(productDto.Description);
        RequestDtoException.ThrowIfLessThan(productDto.Price, 0);
        RequestDtoException.ThrowIfLessThan(productDto.CategoryId, 1);
    }
}