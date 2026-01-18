using Microsoft.AspNetCore.Identity;

namespace LibApp.Domain.Models;

public class Role : IdentityRole<int>
{
    public virtual ICollection<User> Users { get; protected set; } = new List<User>();
}
public enum RoleEnum
{
    Admin = 1,
    Librarian = 2,
    Regular = 3
}