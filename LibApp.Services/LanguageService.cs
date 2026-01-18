using LibApp.Domain.Models;
using LibApp.EfDataAccess;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace LibApp.Services;

public class LanguageService : ILanguageService
{
    private readonly LibraryContext _context;

    public LanguageService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Language>> GetLanguagesAsync()
    {

        var languages = await _context.Languages
            .AsNoTracking()
            .ToListAsync();

        return languages;
    }

    public async Task<Language> GetLanguageAsync(int id)
    {
        var language = await _context.Languages
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        return language;
    }

    public async Task AddLanguageAsync(Language language)
    {
        _context.Add(language);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateLanguageAsync(Language language)
    {
        _context.Update(language);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveLanguageAsync(Language language)
    {
        _context.Remove(language);
        await _context.SaveChangesAsync();
    }

    public bool LanguageExists(string name)
    {
        if (name.IsNullOrEmpty())
            return false;

        var exists = _context.Languages.Any(d => d.Name.ToLower() == name.ToLower());
        return exists;
    }

    public bool LanguageExistsInOtherLanguages(int id, string name)
    {
        var exists = _context.Languages.Any(d => d.Id != id && d.Name.ToLower() == name.ToLower());
        return exists;
    }

    public bool IsDeletable(Language language)
    {
        return !_context.Books.Any(b => b.Language == language);
    }
}