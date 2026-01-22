using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;

namespace LibApp.Services;

public class LanguageService(ILanguageRepository languageRepository) : ILanguageService
{
    public async Task<IEnumerable<Language>> GetLanguagesAsync()
    {
        return await languageRepository.GetAllAsync();
    }

    public async Task<Language?> GetLanguageAsync(int id)
    {
        return await languageRepository.GetByIdAsync(id);
    }

    public async Task AddLanguageAsync(Language language)
    {
        await languageRepository.AddAsync(language);
    }

    public async Task UpdateLanguageAsync(Language language)
    {
        await languageRepository.UpdateAsync(language);
    }

    public async Task RemoveLanguageAsync(Language language)
    {
        await languageRepository.RemoveAsync(language);
    }

    public async Task<bool> LanguageExistsAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;
        
        var languages = await languageRepository.GetAllAsync();
        var exists = languages.Any(language => string.Equals(language.Name, name, StringComparison.OrdinalIgnoreCase));
        return exists;
    }

    public async Task<bool> LanguageExistsInOtherLanguagesAsync(int id, string name)
    {
        if (id == 0 || string.IsNullOrWhiteSpace(name))
            return false;

        var languages = await languageRepository.GetAllAsync();
        var exists = languages.Any(language => language.Id != id && string.Equals(language.Name, name, StringComparison.CurrentCultureIgnoreCase));
        return exists;
    }

    public async Task<bool> IsDeletableAsync(int id)
    {
        if (id == 0) return false;

        var languages = await languageRepository.GetAllAsync();
        return languages.All(language => language.Id != id);
    }
}