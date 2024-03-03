using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly LibraryContext _context;

        public PublisherService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Publisher>> GetPublishersAsync()
        {
            var publishers = await _context.Publishers
                .Include(d => d.CreatedByUser)
                .Include(d => d.ModifiedByUser)
                .AsNoTracking()
                .ToListAsync();

            return publishers;
        }

        public async Task<Publisher> GetPublisherAsync(int id)
        {
            var publisher = await _context.Publishers
                .Include(d => d.CreatedByUser)
                .Include(d => d.ModifiedByUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            return publisher;
        }

        public async Task AddPublisherAsync(Publisher publisher)
        {
            _context.Add(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePublisherAsync(Publisher publisher)
        {
            publisher.ModifiedDateTime = DateTime.Now;

            _context.Update(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePublisherAsync(Publisher publisher)
        {
            _context.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        public bool PublisherExists(string name)
        {
            var exists = _context.Publishers.Any(d => d.Name.ToLower() == name.ToLower());
            return exists;
        }

        public bool PublisherExistsInOtherPublishers(int id, string name)
        {
            var exists = _context.Publishers.Any(d => d.Id != id && d.Name.ToLower() == name.ToLower());
            return exists;
        }

        public bool IsDeletable(Publisher publisher)
        {
            return !_context.Books.Any(b => b.Publisher == publisher);
        }
    }
}
