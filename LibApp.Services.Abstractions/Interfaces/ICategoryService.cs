using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<Category?> GetCategoryAsync(int id);
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task RemoveCategoryAsync(Category category);
    bool CategoryExists(string name);
    bool CategoryExistsInOtherCategories(int id, string name);
    bool IsDeletable(Category category);
}