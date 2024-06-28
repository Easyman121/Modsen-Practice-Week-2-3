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
        var allCats = await DataBase.Category.GetAllAsync(cancellationToken);
        if (allCats.Exists(c => c.Name == categoryDto.Name))
        {
            throw new RequestDtoException("The name is not unique");
        }

        await DataBase.Category.InsertAsync(_mapper.Map<Category>(categoryDto), cancellationToken);
    }

    public async Task UpdateCategoryAsync(int id, CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFields(categoryDto, cancellationToken);
        if (id < 0)
        {
            throw new RequestDtoException("Id must be 0 and higher");
        }

        var cat = await DataBase.Category.GetByIdAsync(id, cancellationToken);
        if (cat == null)
        {
            throw new RequestDtoException("No entries found");
        }

        cat.Name = categoryDto.Name;
        DataBase.Category.UpdateAsync(cat, cancellationToken);
    }

    public async Task<CategoryResponseDto> GetCategoryAsync(int id, CancellationToken cancellationToken)
    {
        if (id < 0)
        {
            throw new RequestDtoException("Id must be 0 and higher");
        }

        var cat = await DataBase.Category.GetByIdAsync(id, cancellationToken);
        if (cat == null)
        {
            throw new RequestDtoException("No entries found");
        }

        return _mapper.Map<CategoryResponseDto>(cat);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetCategoriesAsync(CancellationToken cancellationToken)
    {
        var cats = await DataBase.Category.GetAllAsync(cancellationToken);

        foreach (var cat in cats)
        {
            if (cat == null)
            {
                throw new RequestDtoException("The entry is empty");
            }
        }

        if (cats == null)
        {
            throw new RequestDtoException("The list is empty");
        }

        return _mapper.Map<IEnumerable<CategoryResponseDto>>(cats);
    }

    public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        if (id < 0)
        {
            throw new RequestDtoException("Id must be 0 and higher");
        }

        var cat = await DataBase.Category.GetByIdAsync(id, cancellationToken);
        if (cat == null)
        {
            throw new RequestDtoException("No entries found");
        }

        DataBase.Category.DeleteAsync(cat, cancellationToken);
    }

    public static void CheckFields(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(categoryDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        ArgumentException.ThrowIfNullOrWhiteSpace(categoryDto.Name);
    }
}