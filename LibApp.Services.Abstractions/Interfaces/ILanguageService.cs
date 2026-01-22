using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface ILanguageService
{
    Task<IEnumerable<Language>> GetLanguagesAsync();
    Task<Language?> GetLanguageAsync(int id);
    Task AddLanguageAsync(Language language);
    Task UpdateLanguageAsync(Language language);
    Task RemoveLanguageAsync(Language language);
    Task<bool> LanguageExistsAsync(string name);
    Task<bool> LanguageExistsInOtherLanguagesAsync(int id, string name);
    Task<bool> IsDeletableAsync(int id);
}