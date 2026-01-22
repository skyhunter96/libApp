using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LibApp.Services;

public class PublisherService(IPublisherRepository publisherRepository) : IPublisherService
{
    public async Task<IEnumerable<Publisher>> GetPublishersAsync()
    {
        return await publisherRepository.GetAllWithUsersAsync();
    }

    public async Task<Publisher?> GetPublisherAsync(int id)
    {
        return await publisherRepository.GetByIdWithUsersAsync(id);
    }

    public async Task AddPublisherAsync(Publisher publisher)
    {
        await publisherRepository.AddAsync(publisher);
    }

    public async Task UpdatePublisherAsync(Publisher publisher)
    {
        await publisherRepository.UpdateAsync(publisher);
    }

    public async Task RemovePublisherAsync(Publisher publisher)
    {
        await publisherRepository.RemoveAsync(publisher);
    }

    public async Task<bool> PublisherExistsAsync(string name)
    {
        if (name.IsNullOrEmpty())
            return false;

        return await publisherRepository.AnyAsync(publisher => publisher.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> PublisherExistsInOtherPublishersAsync(int id, string name)
    {
        if (id == 0 || string.IsNullOrWhiteSpace(name))
            return false;

        var publishers = await publisherRepository.GetAllAsync();

        var exists = publishers.Any(d => d.Id != id && d.Name.ToLower().Equals(name, StringComparison.OrdinalIgnoreCase));
        return exists;
    }

    public async Task<bool> IsDeletableAsync(int id)
    {
        if (id == 0) return false;

        var publishers = await publisherRepository.GetAllAsync();
        return publishers.All(b => b.Id != id);
    }
}