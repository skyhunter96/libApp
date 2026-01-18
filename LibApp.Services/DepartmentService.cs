using LibApp.Domain.Models;
using LibApp.EfDataAccess;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace LibApp.Services;

public class DepartmentService : IDepartmentService
{
    private readonly LibraryContext _context;

    public DepartmentService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetDepartmentsAsync()
    {

        var departments = await _context.Departments
            .Include(d => d.CreatedByUser)
            .Include(d => d.ModifiedByUser)
            .AsNoTracking()
            .ToListAsync();

        return departments;
    }

    public async Task<Department?> GetDepartmentAsync(int id)
    {
        var department = await _context.Departments
            .Include(d => d.CreatedByUser)
            .Include(d => d.ModifiedByUser)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        return department;
    }

    public async Task AddDepartmentAsync(Department department)
    {
        _context.Add(department);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDepartmentAsync(Department department)
    {
        department.SetModifiedDateTime(DateTime.Now);

        _context.Update(department);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveDepartmentAsync(Department department)
    {
        _context.Remove(department);
        await _context.SaveChangesAsync();
    }

    public bool DepartmentExists(string name)
    {
        if (name.IsNullOrEmpty())
            return false;

        var exists = _context.Departments.Any(d => d.Name.ToLower() == name.ToLower());
        return exists;
    }

    public bool DepartmentExistsInOtherDepartments(int id, string name)
    {
        var exists = _context.Departments.Any(d => d.Id != id && d.Name.ToLower() == name.ToLower());
        return exists;
    }

    public bool IsDeletable(Department department)
    {
        return !_context.Books.Any(b => b.Department == department);
    }
}