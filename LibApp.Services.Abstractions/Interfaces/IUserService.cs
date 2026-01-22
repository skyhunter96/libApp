using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserAsync(int id);
    Task<User?> GetUserByUserNameAsync(string userName);
    Task AddUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task RemoveUserAsync(User user);
    Task<bool> DocumentIdExistsAsync(string documentId);
    Task<bool> DocumentIdExistsInOtherBooksAsync(int id, string documentId);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> EmailExistsInOtherBooksAsync(int id, string email);
    Task<bool> UserNameExistsAsync(string userName);
    Task<bool> UserNameExistsInOtherBooksAsync(int id, string userName);
    Task ActivateAsync(int id);
    Task DeactivateAsync(int id);
    Task<IEnumerable<Role>> GetRolesAsync();
    Task<Role?> GetRoleForUserIdAsync(int id);
}