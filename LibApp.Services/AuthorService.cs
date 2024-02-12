using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _context;

        public AuthorService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<Author> GetAuthorAsync(int id)
        {
            var author = await _context.Authors
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .FirstOrDefaultAsync(a => a.Id == id);

            return author;
        }

        public Task AddAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
