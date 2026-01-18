using LibApp.Domain.Models;

namespace LibApp.WebApp.Utilities;

public static class AppRoles
{
    public const string Admin = nameof(RoleEnum.Admin);
    public const string Librarian = nameof(RoleEnum.Librarian);
    public const string Regular = nameof(RoleEnum.Regular);
}