﻿using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.DTO.Request;
using BusinessLogicLayer.DTO.Response;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories.Interfaces;

namespace BusinessLogicLayer.Services;

public class CategoryService : ICategoryService
{
    private IUnitOfWork DataBase { get; set; }
    private IMapper _mapper = new MapperConfiguration(x => x.AddProfile<AppMappingProfile>()).CreateMapper();

    public async Task InsertCategoryAsync(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        CheckFields(categoryDto, cancellationToken);
        var allCats = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.Category.GetAllAsync, cancellationToken);

        NonUniqueException.EnsureUnique(allCats, c => c.Name == categoryDto.Name,
            $"Category name {categoryDto.Name} is not unique");

        await DataBase.Category.InsertAsync(_mapper.Map<Category>(categoryDto), cancellationToken);
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
        var cats = await ServiceHelper.CheckAndGetEntitiesAsync(DataBase.Category.GetAllAsync, cancellationToken);

        return _mapper.Map<IEnumerable<CategoryResponseDto>>(cats);
    }

    public async Task DeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        var cat = await ServiceHelper.CheckAndGetEntityAsync(DataBase.Category.GetByIdAsync, id, cancellationToken);

        await DataBase.Category.DeleteAsync(cat, cancellationToken);
    }

    public static void CheckFields(CategoryRequestDto categoryDto, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(categoryDto);
        ArgumentNullException.ThrowIfNull(cancellationToken);
        RequestDtoException.ThrowIfNullOrWhiteSpace(categoryDto.Name);
    }
}