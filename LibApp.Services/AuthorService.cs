using LibApp.Domain.Models;
using LibApp.EfDataAccess;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibApp.Services;

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
        _context.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(Author author)
    {
        try
        {
            author.SetModifiedDateTime(DateTime.UtcNow);

            _context.Update(author);
            await _context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw;
        }
    }

    public async Task RemoveAuthorAsync(Author author)
    {
        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    public bool AuthorExists(string name)
    {
        if (name.IsNullOrEmpty())
            return false;

        var exists = _context.Authors.Any(a => a.Name.ToLower() == name.ToLower());
        return exists;
    }

    public bool AuthorExistsInOtherAuthors(int id, string name)
    {
        var exists = _context.Authors.Any(a => a.Id != id && a.Name.ToLower() == name.ToLower());
        return exists;
    }

    public bool IsDeletable(Author author)
    {
        return !_context.Books.Any(b => b.Authors.Contains(author));
    }
}