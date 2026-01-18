using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface ILanguageService
{
    Task<IEnumerable<Language>> GetLanguagesAsync();
    Task<Language?> GetLanguageAsync(int id);
    Task AddLanguageAsync(Language language);
    Task UpdateLanguageAsync(Language language);
    Task RemoveLanguageAsync(Language language);
    bool LanguageExists(string name);
    bool LanguageExistsInOtherLanguages(int id, string name);
    bool IsDeletable(Language language);
}