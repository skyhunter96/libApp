using LibApp.Domain.Models;
using LibApp.EfDataAccess;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Services;

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
            .Include(b => b.Publisher)
            .AsNoTracking();

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
            .AsNoTracking()
            .ToListAsync();

        return books;
    }

    public async Task<Book?> GetBookAsync(int id)
    {
        var book = await _context.Books
            .Include(b => b.Authors)
            .Include(b => b.CreatedByUser)
            .Include(b => b.ModifiedByUser)
            .Include(b => b.Category)
            .Include(b => b.Department)
            .Include(b => b.Language)
            .Include(b => b.Publisher)
            .Include(b => b.BookReservations)
            .FirstOrDefaultAsync(b => b.Id == id);

        return book;
    }

    public IEnumerable<int> GetBookIdsByAuthorId(int authorId)
    {
        return _context.Authors
            .Where(a => a.Id == authorId)
            .SelectMany(a => a.Books.Select(b => b.Id))
            .Distinct()
            .ToList();
    }

    public async Task AddBookAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName)
    {
        var modifiedByUserId = book.ModifiedByUserId;
        var newAuthor = new Author();
        newAuthor.SetCreatedByUserId(modifiedByUserId);
        newAuthor.SetModifiedByUserId(modifiedByUserId);

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

        book.AddAuthors(existingAuthors);

        _context.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(Book book, IEnumerable<int>? selectedAuthorIds, string? newAuthorName)
    {
        var newAuthor = new Author();
        newAuthor.SetCreatedByUserId(book.ModifiedByUserId);
        newAuthor.SetModifiedByUserId(book.ModifiedByUserId);

        // Like this cuz plain 'book' does not get authors loaded
        var bookToUpdate = await GetBookAsync(book.Id);

        if (bookToUpdate == null)
        {
            throw new InvalidOperationException($"Book with Id {book.Id} not found.");
        }

        // Detach the entity from the context
        _context.Entry(bookToUpdate).State = EntityState.Detached;

        // Attach the provided 'bookToUpdate' to the context
        _context.Attach(bookToUpdate);

        bookToUpdate.SetModifiedByUserId(book.ModifiedByUserId);

        await MapBook(book, bookToUpdate, _context);

        if (newAuthorName != null)
        {
            newAuthor.Name = newAuthorName;

            _context.Authors.Add(newAuthor);
            await _context.SaveChangesAsync();
        }

        var existingAuthors = new List<Author>();

        if (selectedAuthorIds != null)
        {
            existingAuthors = await _context.Authors.Where(a => selectedAuthorIds.Contains(a.Id)).ToListAsync();
        }

        if (newAuthor.Id != 0)
        {
            existingAuthors.Add(newAuthor);
        }

        bookToUpdate.AddAuthors(existingAuthors);
        bookToUpdate.SetModifiedDateTime(DateTime.Now);

        _context.Update(bookToUpdate);
        await _context.SaveChangesAsync();
    }

    private static async Task MapBook(Book book, Book? bookToUpdate, LibraryContext context)
    {
        if (bookToUpdate == null)
        {
            throw new ArgumentNullException(nameof(bookToUpdate));
        }

        bookToUpdate.Title = book.Title;
        bookToUpdate.Description = book.Description;
        bookToUpdate.Isbn = book.Isbn;
        bookToUpdate.Edition = book.Edition;
        bookToUpdate.ReleaseYear = book.ReleaseYear;

        if (book.PublisherId != 0)
        {
            var newPublisher = await context.Publishers.FindAsync(book.PublisherId);
            bookToUpdate.Publisher = newPublisher!;
        }

        if (book.CategoryId != 0)
        {
            var newCategory = await context.Categories.FindAsync(book.CategoryId);
            bookToUpdate.Category = newCategory!;
        }

        if (book.DepartmentId != 0)
        {
            var newDepartment = await context.Departments.FindAsync(book.DepartmentId);
            bookToUpdate.Department = newDepartment!;
        }

        if (book.LanguageId != 0)
        {
            var newLanguage = await context.Languages.FindAsync(book.LanguageId);
            bookToUpdate.Language = newLanguage!;
        }

        bookToUpdate.ImagePath = book.ImagePath;
        bookToUpdate.Cost = book.Cost;
        bookToUpdate.IsAvailable = book.IsAvailable;
        bookToUpdate.Quantity = book.Quantity;
        bookToUpdate.AvailableQuantity = book.AvailableQuantity;
        bookToUpdate.ReservedQuantity = book.ReservedQuantity;
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

    public bool IsbnExistsInOtherBooks(int id, string isbn)
    {
        var exists = _context.Books.Any(b => b.Id != id && b.Isbn == isbn);
        return exists;
    }
}