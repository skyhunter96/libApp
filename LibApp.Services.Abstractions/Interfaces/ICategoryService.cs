using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryAsync(int id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task RemoveCategoryAsync(Category category);
    Task<bool> CategoryExistsAsync(string name);
    Task<bool> CategoryExistsInOtherCategoriesAsync(int id, string name);
    Task<bool> IsDeletableAsync(int id);
}