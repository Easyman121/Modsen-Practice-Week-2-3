using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class CategoryService(IUnitOfWork DataBase) : ICategoryService
{
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task<int> InsertCategoryAsync(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFields(categoryDto, cancellationToken);
        var allCats = await ServiceHelper.GetEntitiesAsync(DataBase.Category.GetAllAsync, cancellationToken);

        if (allCats.Count != 0)
        {
            NonUniqueException.EnsureUnique(allCats, c => c.Name == categoryDto.Name,
                $"Category name {categoryDto.Name} is not unique");
        }

        return await DataBase.Category.InsertAsync(_mapper.Map<Category>(categoryDto), cancellationToken);
    }

    public async Task UpdateCategoryAsync(int id, CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFields(categoryDto, cancellationToken);

        var allCats = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.Category.GetAllAsync, cancellationToken);

        NonUniqueException.EnsureUnique(allCats, c => c.Name == categoryDto.Name,
            $"Category name {categoryDto.Name} is not unique");

        var cat = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Category.GetByIdAsync, id, cancellationToken);
        cat.Name = categoryDto.Name;

        await DataBase.Category.UpdateAsync(cat, cancellationToken);
    }

    public async Task<CategoryResponseDto> GetCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var cat = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Category.GetByIdAsync, id, cancellationToken);

        return _mapper.Map<CategoryResponseDto>(cat);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var cats = await ServiceHelper.GetEntitiesAsync(DataBase.Category.GetAllAsync, cancellationToken);

        return _mapper.Map<IEnumerable<CategoryResponseDto>>(cats);
    }

    public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var cat = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Category.GetByIdAsync, id, cancellationToken);

        await DataBase.Category.DeleteAsync(cat, cancellationToken);
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProductsAsync(int categoryId,
        CancellationToken cancellationToken)
    {
        var cat = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Category.GetByIdAsync, categoryId,
            cancellationToken);

        return _mapper.Map<IEnumerable<ProductResponseDto>>(cat.Products);
    }

    public static void CheckFields(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(categoryDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(categoryDto.Name);
    }
}