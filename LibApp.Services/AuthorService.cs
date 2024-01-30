using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;

namespace LibApp.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _context;

        public AuthorService(LibraryContext context)
        {
            _context = context;
        }

        public Task AddAuthorAsync(Author author)
        {
            throw new NotImplementedException();
        }
    }
}
