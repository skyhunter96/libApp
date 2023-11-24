using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = _context.Books
                .Include(b => b.Authors)
                .Include(b => b.CreatedByUser)
                .Include(b => b.ModifiedByUser)
                .Include(b => b.Category)
                .Include(b => b.Department)
                .Include(b => b.Language)
                .Include(b => b.Publisher);

            return books;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var books = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Category)
                .Include(b => b.Department)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .ToListAsync();

            return books;
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.CreatedByUser)
                .Include(b => b.ModifiedByUser)
                .Include(b => b.Category)
                .Include(b => b.Department)
                .Include(b => b.Language)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);

            return book;
        }

        public bool IsbnExists(string isbn)
        {
            var book = _context.Books.FirstOrDefault(b => b.Isbn == isbn);

            return book != null;
        }

        public async Task AddBookAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthor)
        {
            //TODO: Handle newAuthor
            //TODO: Modified dateTime and createdDateTime? prolly not

            book.CreatedByUserId = 1;
            book.ModifiedByUserId = 1;

            if (existingAuthorIds != null)
            {
                var existingAuthors = await _context.Authors.Where(a => existingAuthorIds.Contains(a.Id)).ToListAsync();

                book.Authors = existingAuthors;
            }

            _context.Add(book);

            await _context.SaveChangesAsync();
        }
    }
}