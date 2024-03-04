using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Services
{
    public class UserService : IUserService
    {
        private readonly LibraryContext _context;
        private readonly UserManager<User> _userManager;

        public UserService(LibraryContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.CreatedByUser)
                .Include(u => u.ModifiedByUser)
                .Include(u => u.Role)
                .AsNoTracking()
                .ToListAsync();

            return users;
        }

        public async Task<User> GetUserAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.CreatedByUser)
                .Include(u => u.ModifiedByUser)
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            var user = await _context.Users
                .Include(u => u.CreatedByUser)
                .Include(u => u.ModifiedByUser)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == userName);

            return user;
        }

        public async Task AddUserAsync(User user)
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.Password);

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ApplicationException($"User update failed: {string.Join(", ", errors)}");
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            user.ModifiedDateTime = DateTime.Now;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ApplicationException($"User update failed: {string.Join(", ", errors)}");
            }
        }

        public async Task RemoveUserAsync(User user)
        {
            try
            {
                await SeverRelations(user);

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private async Task SeverRelations(User user)
        {
            foreach (var book in _context.Books.Where(b => b.CreatedByUserId == user.Id))
            {
                book.CreatedByUser = null;
            }

            foreach (var book in _context.Books.Where(b => b.ModifiedByUserId == user.Id))
            {
                book.ModifiedByUser = null;
            }

            foreach (var author in _context.Authors.Where(b => b.CreatedByUserId == user.Id))
            {
                author.CreatedByUser = null;
            }

            foreach (var author in _context.Authors.Where(b => b.ModifiedByUserId == user.Id))
            {
                author.ModifiedByUser = null;
            }

            foreach (var publisher in _context.Publishers.Where(b => b.CreatedByUserId == user.Id))
            {
                publisher.CreatedByUser = null;
            }

            foreach (var publisher in _context.Publishers.Where(b => b.ModifiedByUserId == user.Id))
            {
                publisher.ModifiedByUser = null;
            }

            foreach (var department in _context.Departments.Where(b => b.CreatedByUserId == user.Id))
            {
                department.CreatedByUser = null;
            }

            foreach (var department in _context.Departments.Where(b => b.ModifiedByUserId == user.Id))
            {
                department.ModifiedByUser = null;
            }

            foreach (var reservation in _context.Reservations.Where(b => b.CreatedByUserId == user.Id))
            {
                reservation.CreatedByUser = null;
            }

            foreach (var reservation in _context.Reservations.Where(b => b.ModifiedByUserId == user.Id))
            {
                reservation.ModifiedByUser = null;
            }

            foreach (var userToChange in _context.Users.Where(b => b.CreatedByUserId == user.Id))
            {
                userToChange.CreatedByUser = null;
            }

            foreach (var userToChange in _context.Users.Where(b => b.ModifiedByUserId == user.Id))
            {
                userToChange.ModifiedByUser = null;
            }

            await _context.SaveChangesAsync();
        }

        public bool DocumentIdExists(string documentId)
        {
            var exists = _context.Users.Any(u => u.DocumentId == documentId);
            return exists;
        }

        public bool DocumentIdExistsInOtherBooks(int id, string documentId)
        {
            var exists = _context.Users.Any(u => u.Id != id && u.DocumentId == documentId);
            return exists;
        }

        public bool EmailExists(string email)
        {
            var exists = _context.Users.Any(u => u.Email == email);
            return exists;
        }

        public bool EmailExistsInOtherBooks(int id, string email)
        {
            var exists = _context.Users.Any(u => u.Id != id && u.Email == email);
            return exists;
        }

        public bool UserNameExists(string userName)
        {
            var exists = _context.Users.Any(u => u.UserName == userName);
            return exists;
        }

        public bool UserNameExistsInOtherBooks(int id, string userName)
        {
            var exists = _context.Users.Any(u => u.Id != id && u.UserName == userName);
            return exists;
        }

        public void Activate(int id)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == id);

            if (user == null) return;

            user.IsActive = true;

            _context.SaveChangesAsync();
        }

        public void Deactivate(int id)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Id == id);

            if (user == null) return;

            user.IsActive = false;

            _context.SaveChangesAsync();
        }
    }
}
