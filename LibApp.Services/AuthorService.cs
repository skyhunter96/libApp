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

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            var authors = await _context.Authors
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .AsNoTracking()
                .ToListAsync();

            return authors;
        }

        public async Task<Author> GetAuthorAsync(int id)
        {
            var author = await _context.Authors
                .Include(a => a.CreatedByUser)
                .Include(a => a.ModifiedByUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return author;
        }

        public async Task AddAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
