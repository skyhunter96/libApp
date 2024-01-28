using Domain.Models;

namespace LibApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task RemoveUserAsync(User user);
        bool DocumentIdExists(string documentId);
        bool EmailExists(string email);
        bool UserNameExists(string userName);
    }
}
