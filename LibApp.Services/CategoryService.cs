using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;

namespace LibApp.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        return await categoryRepository.GetAllWithUsersAsync();
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        return await categoryRepository.GetByIdWithUsersAsync(id);
    }

    public async Task AddCategoryAsync(Category category)
    {
        await categoryRepository.AddAsync(category);
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        await categoryRepository.UpdateAsync(category);
    }

    public async Task RemoveCategoryAsync(Category category)
    {
        await categoryRepository.RemoveAsync(category);
    }

    public async Task<bool> CategoryExistsAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        return await categoryRepository.AnyAsync(category => category.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> CategoryExistsInOtherCategoriesAsync(int id, string name)
    {
        if (id == 0 || string.IsNullOrWhiteSpace(name))
            return false;

        var categories = await categoryRepository.GetAllAsync();
        var exists = categories.Any(category => category.Id != id && string.Equals(category.Name, name, StringComparison.OrdinalIgnoreCase));
        return exists;
    }

    public async Task<bool> IsDeletableAsync(int id)
    {
        if (id == 0) return false;

        var categories = await categoryRepository.GetAllAsync();
        return categories.All(category => category.Id != id);
    }
}