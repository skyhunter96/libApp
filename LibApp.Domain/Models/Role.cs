using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class Role : IdentityRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }
    }
    public enum RoleEnum
    {
        Admin = 1,
        Librarian = 2,
        Regular = 3
    }
}
