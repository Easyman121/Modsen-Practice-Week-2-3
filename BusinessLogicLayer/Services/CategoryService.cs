using AutoMapper;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class CategoryService(IUnitOfWork uow, IMapper mapper) : ICategoryService
{
    public async Task<int> InsertCategoryAsync(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(categoryDto, cancellationToken);
        var allCats = await ServiceHelper.GetEntitiesAsync(uow.Category.GetAllAsync, cancellationToken);

        if (allCats.Count > 0)
        {
            NonUniqueException.EnsureUnique(allCats, c => c.Name == categoryDto.Name,
                $"Category name {categoryDto.Name} is not unique");
        }

        var cat = mapper.Map<Category>(categoryDto);
        return await uow.Category.InsertAsync(cat, cancellationToken);
    }

    public async Task UpdateCategoryAsync(int id, CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFieldsAndToken(categoryDto, cancellationToken);

        var allCats = await ServiceHelper.GetEntitiesAsync(uow.Category.GetAllAsync, cancellationToken);
        NonUniqueException.EnsureUnique(allCats, c => c.Name == categoryDto.Name,
            $"Category name {categoryDto.Name} is not unique");

        var cat = await ServiceHelper.CheckAndGetEntityAsync(uow.Category.GetByIdAsync, id, cancellationToken);
        cat.Name = categoryDto.Name;
        await uow.Category.UpdateAsync(cat, cancellationToken);
    }

    public async Task<CategoryResponseDto> GetCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var cat = await ServiceHelper.CheckAndGetEntityAsync(uow.Category.GetByIdAsync, id, cancellationToken);

        return mapper.Map<CategoryResponseDto>(cat);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var cats = await ServiceHelper.GetEntitiesAsync(uow.Category.GetAllAsync, cancellationToken);

        return mapper.Map<IEnumerable<CategoryResponseDto>>(cats);
    }

    public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var cat = await ServiceHelper.CheckAndGetEntityAsync(uow.Category.GetByIdAsync, id, cancellationToken);

        await uow.Category.DeleteAsync(cat, cancellationToken);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(int categoryId,
        CancellationToken cancellationToken)
    {
        var cat = await ServiceHelper.CheckAndGetEntityAsync(uow.Category.GetByIdAsync, categoryId,
            cancellationToken);

        return mapper.Map<IEnumerable<ProductResponseDto>>(cat.Products);
    }

    public static void CheckFieldsAndToken(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(categoryDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(categoryDto.Name);
    }
}