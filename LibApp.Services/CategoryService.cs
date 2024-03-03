using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Services
{
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

        public async Task<Category> GetCategoryAsync(int id)
        {
            var category = await _context.Categories
                .Include(d => d.CreatedByUser)
                .Include(d => d.ModifiedByUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            return category;
        }
    }
}
