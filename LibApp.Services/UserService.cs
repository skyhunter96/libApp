using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibApp.Services;

public class UserService(IUserRepository userRepository, UserManager<User> userManager) : IUserService
{
    private IEnumerable<User>? _cachedUsers;

    private async Task<IEnumerable<User>> GetCachedUsersAsync()
    {
        if (_cachedUsers != null)
            return _cachedUsers;

        _cachedUsers = (await userRepository.GetAllAsync()).ToList();
        return _cachedUsers;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await userRepository.GetAllWithRolesAsync();
    }

    public async Task<User?> GetUserAsync(int id)
    {
        return await userRepository.GetByIdWithRolesAsync(id);
    }

    public async Task<User?> GetUserByUserNameAsync(string userName)
    {
        var users = await userRepository.GetAllWithRolesAsync();
        var user = users.FirstOrDefault(u => u.UserName == userName);

        return user;
    }

    public async Task AddUserAsync(User user)
    {
        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, user.Password);

        var result = await userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            throw new ApplicationException($"User update failed: {string.Join(", ", errors)}");
        }
    }

    public async Task UpdateUserAsync(User user)
    {
        user.SetModifiedDateTime(DateTime.Now);

        var result = await userManager.UpdateAsync(user);

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
            await userRepository.SeverRelationsAsync(user);
            await userRepository.RemoveAsync(user);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }

    public async Task<bool> DocumentIdExistsAsync(string documentId)
    {
        if (string.IsNullOrEmpty(documentId))
            return false;

        var users = await GetCachedUsersAsync();
        return users.Any(u => u.DocumentId == documentId);
    }

    

    public async Task<bool> DocumentIdExistsInOtherBooksAsync(int id, string documentId)
    {
        if (id == 0 || string.IsNullOrWhiteSpace(documentId))
            return false;

        var users = await GetCachedUsersAsync();
        return users.Any(u => u.Id != id && u.DocumentId == documentId);
    }
    //public async Task<bool> DocumentIdExistsAsync(string documentId)
    //{
    //    if(string.IsNullOrEmpty(documentId))
    //        return false;

    //    var users = await userRepository.GetAllAsync();
    //    var exists = users.Any(u => u.DocumentId == documentId);
    //    return exists;
    //}
    //public async Task<bool> DocumentIdExistsInOtherBooksAsync(int id, string documentId)
    //{
    //    if (id == 0 || string.IsNullOrWhiteSpace(documentId))
    //        return false;

    //    var users = await userRepository.GetAllAsync();
    //    var exists = users.Any(u => u.Id != id && u.DocumentId == documentId);
    //    return exists;
    //}

    public async Task<bool> EmailExistsAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        var users = await GetCachedUsersAsync();
        return users.Any(u => u.Email == email);
    }

    public async Task<bool> EmailExistsInOtherBooksAsync(int id, string email)
    {
        if (id == 0 || string.IsNullOrWhiteSpace(email))
            return false;

        var users = await GetCachedUsersAsync();
        return users.Any(u => u.Id != id && u.Email == email);
    }

    //public async Task<bool> EmailExistsAsync(string email)
    //{
    //    if (string.IsNullOrEmpty(email))
    //        return false;

    //    var users = await userRepository.GetAllAsync();
    //    var exists = users.Any(u => u.Email == email);
    //    return exists;
    //}

    //public async Task<bool> EmailExistsInOtherBooksAsync(int id, string email)
    //{
    //    if (id == 0 || string.IsNullOrWhiteSpace(email))
    //        return false;

    //    var users = await userRepository.GetAllAsync();
    //    var exists = users.Any(u => u.Id != id && u.Email == email);
    //    return exists;
    //}

    public async Task<bool> UserNameExistsAsync(string userName)
    {
        if (string.IsNullOrEmpty(userName))
            return false;

        var users = await GetCachedUsersAsync();
        return users.Any(u => u.UserName == userName);
    }

    public async Task<bool> UserNameExistsInOtherBooksAsync(int id, string userName)
    {
        if (id == 0 || string.IsNullOrWhiteSpace(userName))
            return false;

        var users = await GetCachedUsersAsync();
        return users.Any(u => u.Id != id && u.UserName == userName);
    }
    //public async Task<bool> UserNameExistsAsync(string userName)
    //{
    //    if (string.IsNullOrEmpty(userName))
    //        return false;

    //    var users = await userRepository.GetAllAsync();
    //    var exists = users.Any(u => u.UserName == userName);
    //    return exists;
    //}

    //public async Task<bool> UserNameExistsInOtherBooksAsync(int id, string userName)
    //{
    //    if (id == 0 || string.IsNullOrWhiteSpace(userName))
    //        return false;

    //    var users = await userRepository.GetAllAsync();
    //    var exists = users.Any(u => u.Id != id && u.UserName == userName);
    //    return exists;
    //}

    public async Task ActivateAsync(int id)
    {
        var user = await GetUserAsync(id);
        if (user == null) return;

        user.IsActive = true;
        await userRepository.UpdateAsync(user);
    }

    public async Task DeactivateAsync(int id)
    {
        var user = await GetUserAsync(id);
        if (user == null) return;

        user.IsActive = false;
        await userRepository.UpdateAsync(user);
    }

    public async Task<IEnumerable<Role>> GetRolesAsync()
    {
        return await userRepository.GetAllRolesAsync();
    }

    public async Task<Role?> GetRoleForUserIdAsync(int id)
    {
        return await userRepository.GetRoleForUserIdAsync(id);
    }
}