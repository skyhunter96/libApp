using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;

        public UserService(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.CreatedByUser)
                .Include(u => u.ModifiedByUser)
                .Include(u => u.Role)
                .ToListAsync();

            return users;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.CreatedByUser)
                .Include(u => u.ModifiedByUser)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);

            return user;
        }

        public async Task AddUserAsync(User User)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(User User)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveUserAsync(User User)
        {
            throw new NotImplementedException();
        }
    }
}
