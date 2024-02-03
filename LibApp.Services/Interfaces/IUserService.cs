using Domain.Models;

namespace LibApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserByUserNameAsync(string userName);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task RemoveUserAsync(User user);
        bool DocumentIdExists(string documentId);
        bool DocumentIdExistsInOtherBooks(int id, string documentId);
        bool EmailExists(string email);
        bool EmailExistsInOtherBooks(int id, string email);
        bool UserNameExists(string userName);
        bool UserNameExistsInOtherBooks(int id, string userName);
        void Activate(int id);
        void Deactivate(int id);
    }
}
