using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

internal class CategoryService : ICategoryService
{
    private IUnitOfWork DataBase { get; set; }

    public async Task InsertCategoryAsync(CategoryRequestDto categoryDto, CancellationToken cancellationToken) =>
        ArgumentNullException.ThrowIfNull(categoryDto);

    public async Task SetCategoryAsync(CategoryRequestDto? categoryDto, CancellationToken cancellationToken)
    {
        if (categoryDto == null)
        {
            throw new Exception("ebanyi rot");
        }

        if (DataBase.Category == null)
        {
            throw new Exception("ebanyi rot");
        }

        await DataBase.Category.InsertAsync(cat, cancellationToken);
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

    public static void CheckFields(CategoryRequestDto categoryDto)
    {
        ArgumentNullException.ThrowIfNull(categoryDto);
        ArgumentException.ThrowIfNullOrWhiteSpace(categoryDto.Name);
    }
}