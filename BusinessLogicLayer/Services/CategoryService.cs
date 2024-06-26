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

    public async Task InsertCategoryAsync(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFields(categoryDto, cancellationToken);
        List<Category> a = await DataBase.Category.GetAllAsync(cancellationToken);
        if (a.Exists(c => c.Name == categoryDto.Name))
        {
            throw new RequestDtoException("The name is not unique");
        }

        await DataBase.Category.InsertAsync(a[0], cancellationToken);
        //await DataBase.Category.InsertAsync(cat, cancellationToken);
    }

    public async Task<CategoryResponseDto> GetCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var cat = await DataBase.Category.GetByIdAsync(id, cancellationToken);
        var catResponse = new CategoryResponseDto { Id = cat.Id, Name = cat.Name };
        return catResponse;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var cats = await DataBase.Category.GetAllAsync(cancellationToken);
        var catsResponse = cats.Select(cat => new CategoryResponseDto() { Id = cat.Id, Name = cat.Name });
        return catsResponse;
    }

    public static void CheckFields(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(categoryDto);

        ArgumentNullException.ThrowIfNull(cancellationToken);

        //var Name = categoryDto.Name;
        ArgumentException.ThrowIfNullOrWhiteSpace(categoryDto.Name);
        /*
        if (Name.Length > 32)
        {
            throw new RequestDtoException("Name exceeds 32 characters");
        }

        string pattern = @"^[A-Z][a-z\-_][0-9]*$";
        if (!Regex.IsMatch(Name, pattern))
            throw new RequestDtoException("Name can only contain latin characters,numbers, dashes and underscores");*/
    }
}