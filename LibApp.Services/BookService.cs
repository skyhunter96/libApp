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

        public async Task AddBookAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName)
        {
            var newAuthor = new Author();

            book.CreatedByUserId = book.ModifiedByUserId = newAuthor.CreatedByUserId = newAuthor.ModifiedByUserId = 1;

            if (newAuthorName != null)
            {
                newAuthor.Name = newAuthorName;

                _context.Authors.Add(newAuthor);
                await _context.SaveChangesAsync();
            }

            var existingAuthors = new List<Author>();

            if (existingAuthorIds != null)
            {
                existingAuthors = await _context.Authors.Where(a => existingAuthorIds.Contains(a.Id)).ToListAsync();
            }

            if (newAuthor.Id != 0)
            {
                existingAuthors.Add(newAuthor);
            }

            book.Authors = existingAuthors;

            _context.Add(book);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveBookAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public bool IsbnExists(string isbn)
        {
            var book = _context.Books.FirstOrDefault(b => b.Isbn == isbn);

            return book != null;
        }
    }
}