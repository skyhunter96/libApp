using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.EfDataAccess.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace LibApp.EfDataAccess.Repositories;

public class BookRepository(LibraryContext context) : GenericRepository<Book>(context), IBookRepository
{
    public async Task<IEnumerable<Book>> GetAllWithRelatedEntitiesAsync()
    {
        return await Query()
            .Include(book => book.CreatedByUser)
            .Include(book => book.ModifiedByUser)
            .Include(book => book.Authors)
            .Include(book => book.Category)
            .Include(book => book.Department)
            .Include(book => book.Language)
            .Include(book => book.Publisher)
            .Include(book => book.BookReservations)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdWithUsersAsync(int id)
    {
        return await Query()
            .Include(book => book.CreatedByUser)
            .Include(book => book.ModifiedByUser)
            .Include(book => book.Authors)
            .Include(book => book.Category)
            .Include(book => book.Department)
            .Include(book => book.Language)
            .Include(book => book.Publisher)
            .Include(book => book.BookReservations)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<int>> GetBookIdsByAuthorIdAsync(int authorId)
    {
        return await Query()
            .Where(book => book.Authors.Any(author => author.Id == authorId))
            .Select(book => book.Id)
            .Distinct()
            .ToListAsync();
    }

    public async Task AddAsync(Book book, IEnumerable<int>? existingAuthorIds, string? newAuthorName)
    {
        var modifiedByUserId = book.ModifiedByUserId;
        var newAuthor = new Author();
        newAuthor.SetCreatedByUserId(modifiedByUserId);
        newAuthor.SetModifiedByUserId(modifiedByUserId);

        if (newAuthorName != null)
        {
            newAuthor.Name = newAuthorName;

            context.Authors.Add(newAuthor);
            await context.SaveChangesAsync();
        }

        var existingAuthors = new List<Author>();

        if (existingAuthorIds != null)
        {
            existingAuthors = await context.Authors.Where(author => existingAuthorIds.Contains(author.Id)).ToListAsync();
        }

        if (newAuthor.Id != 0)
        {
            existingAuthors.Add(newAuthor);
        }

        book.AddAuthors(existingAuthors);

        context.Add(book);
        await context.SaveChangesAsync();
    }

    // Updated to manipulate the tracked Book.Authors collection by Id to avoid duplicate join inserts.
    public async Task UpdateAsync(Book book, IEnumerable<int>? selectedAuthorIds, string? newAuthorName)
    {
        // Load the tracked book including current authors
        var bookToUpdate = await context.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == book.Id);

        if (bookToUpdate == null)
        {
            throw new InvalidOperationException($"Book with Id {book.Id} not found.");
        }

        // Map scalar/related navigation properties
        await MapBook(book, bookToUpdate, context);

        // Handle optional new author creation
        Author? newAuthor = null;
        if (!string.IsNullOrWhiteSpace(newAuthorName))
        {
            newAuthor = new Author
            {
                Name = newAuthorName
            };
            newAuthor.SetCreatedByUserId(book.ModifiedByUserId);
            newAuthor.SetModifiedByUserId(book.ModifiedByUserId);
            context.Authors.Add(newAuthor);
            await context.SaveChangesAsync(); // ensure newAuthor.Id is populated
        }

        // Build desired author id set
        var desiredAuthorIds = (selectedAuthorIds ?? Enumerable.Empty<int>()).ToList();
        if (newAuthor != null)
            desiredAuthorIds.Add(newAuthor.Id);

        // Current author ids attached to book
        var currentAuthorIds = bookToUpdate.Authors.Select(a => a.Id).ToList();

        // Remove associations that are no longer desired
        var authorsToRemove = bookToUpdate.Authors.Where(a => !desiredAuthorIds.Contains(a.Id)).ToList();
        foreach (var author in authorsToRemove)
        {
            bookToUpdate.Authors.Remove(author);
        }

        // Add missing associations (use tracked Author entities from context to avoid duplicate inserts)
        var authorsToAddIds = desiredAuthorIds.Except(currentAuthorIds).ToList();
        if (authorsToAddIds.Any())
        {
            var authorsToAdd = await context.Authors.Where(a => authorsToAddIds.Contains(a.Id)).ToListAsync();
            foreach (var author in authorsToAdd)
            {
                // guard against accidental duplicates (shouldn't be needed, but safe)
                if (bookToUpdate.Authors.All(existingAuthor => existingAuthor.Id != author.Id))
                    bookToUpdate.Authors.Add(author);
            }
        }

        bookToUpdate.SetModifiedDateTime(DateTime.Now);

        // Save all changes in one SaveChangesAsync call to let EF update join table correctly
        context.Update(bookToUpdate);
        await context.SaveChangesAsync();
    }

    private static async Task MapBook(Book book, Book? bookToUpdate, LibraryContext context)
    {
        if (bookToUpdate == null)
        {
            throw new ArgumentNullException(nameof(bookToUpdate));
        }

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

        bookToUpdate.Title = book.Title;
        bookToUpdate.Description = book.Description;
        bookToUpdate.Isbn = book.Isbn;
        bookToUpdate.Edition = book.Edition;
        bookToUpdate.ReleaseYear = book.ReleaseYear;
        bookToUpdate.ImagePath = book.ImagePath;
        bookToUpdate.Cost = book.Cost;
        bookToUpdate.IsAvailable = book.IsAvailable;
        bookToUpdate.Quantity = book.Quantity;
        bookToUpdate.AvailableQuantity = book.AvailableQuantity;
        bookToUpdate.ReservedQuantity = book.ReservedQuantity;
        bookToUpdate.SetCreatedByUserId(book.CreatedByUserId);
        bookToUpdate.SetModifiedByUserId(book.ModifiedByUserId);
    }
}
