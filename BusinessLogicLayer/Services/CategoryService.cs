using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

internal class CategoryService : ICategoryService
{
    private IUnitOfWork DataBase { get; set; }
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task InsertCategoryAsync(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFields(categoryDto, cancellationToken);
        var allCats = await CheckAndGetCategoriesAsync(cancellationToken);
        if (allCats.Exists(c => c.Name == categoryDto.Name))
        {
            throw new NonUniqueException("The name is not unique");
        }

        await DataBase.Category.InsertAsync(_mapper.Map<Category>(categoryDto), cancellationToken);
    }

    public async Task UpdateCategoryAsync(int id, CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFields(categoryDto, cancellationToken);
        var cat = await CheckAndGetCategoryAsync(id, cancellationToken);
        cat.Name = categoryDto.Name;
        DataBase.Category.UpdateAsync(cat, cancellationToken);
    }

    public async Task<CategoryResponseDto> GetCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var cat = await CheckAndGetCategoryAsync(id, cancellationToken);

        return _mapper.Map<CategoryResponseDto>(cat);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var cats = await DataBase.Category.GetAllAsync(cancellationToken);

        if (cats.Count == 0)
        {
            throw new RequestDtoException("The list is empty");
        }

        return _mapper.Map<IEnumerable<CategoryResponseDto>>(cats);
    }

    public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var cat = await CheckAndGetCategoryAsync(id, cancellationToken);

        DataBase.Category.DeleteAsync(cat, cancellationToken);
    }

    public static void CheckFields(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(categoryDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(categoryDto.Name);
    }

    private async Task<Category> CheckAndGetCategoryAsync(int id, CancellationToken cancellationToken)
    {
        RequestDtoException.ThrowIfLessThan(id, 0);

        var cat = await DataBase.Category.GetByIdAsync(id, cancellationToken);
        if (cat == null)
        {
            throw new RequestDtoException("No category entries found");
        }


        return cat;
    }

    private async Task<List<Category>> CheckAndGetCategoriesAsync(CancellationToken cancellationToken)
    {
        var cats = await DataBase.Category.GetAllAsync(cancellationToken);

        if (cats.Count == 0)
        {
            throw new RequestDtoException("The list is empty");
        }

        return cats;
    }
}