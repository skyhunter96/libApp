using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class Role : IdentityRole<int>
    {
        public User User { get; set; }
    }
    public enum RoleEnum
    {
        Admin = 1,
        Librarian = 2,
        Regular = 3
    }
}
