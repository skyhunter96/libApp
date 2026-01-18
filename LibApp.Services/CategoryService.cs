using LibApp.Domain.Models;
using LibApp.EfDataAccess;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibApp.Services;

public class CategoryService : ICategoryService
{
    private readonly LibraryContext _context;

    public CategoryService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
        var categories = await _context.Categories
            .Include(d => d.CreatedByUser)
            .Include(d => d.ModifiedByUser)
            .AsNoTracking()
            .ToListAsync();

        return categories;
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        var category = await _context.Categories
            .Include(d => d.CreatedByUser)
            .Include(d => d.ModifiedByUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        return category;
    }

    public async Task AddCategoryAsync(Category category)
    {
        _context.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        category.SetModifiedDateTime(DateTime.Now);

        _context.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveCategoryAsync(Category category)
    {
        _context.Remove(category);
        await _context.SaveChangesAsync();
    }

    public bool CategoryExists(string name)
    {
        if (name.IsNullOrEmpty())
            return false;

        var exists = _context.Categories.Any(d => d.Name.ToLower() == name.ToLower());
        return exists;
    }

    public bool CategoryExistsInOtherCategories(int id, string name)
    {
        var exists = _context.Categories.Any(d => d.Id != id && d.Name.ToLower() == name.ToLower());
        return exists;
    }

    public bool IsDeletable(Category category)
    {
        return !_context.Books.Any(b => b.Category == category);
    }
}