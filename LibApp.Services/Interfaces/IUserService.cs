using Domain.Models;

namespace LibApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task AddUserAsync(User User);
        Task UpdateUserAsync(User User);
        Task RemoveUserAsync(User User);
    }
}
