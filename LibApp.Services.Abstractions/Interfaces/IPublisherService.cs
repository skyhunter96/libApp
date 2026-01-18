using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface IPublisherService
{
    Task<IEnumerable<Publisher>> GetPublishersAsync();
    Task<Publisher?> GetPublisherAsync(int id);
    Task AddPublisherAsync(Publisher publisher);
    Task UpdatePublisherAsync(Publisher publisher);
    Task RemovePublisherAsync(Publisher publisher);
    bool PublisherExists(string name);
    bool PublisherExistsInOtherPublishers(int id, string name);
    bool IsDeletable(Publisher publisher);
}