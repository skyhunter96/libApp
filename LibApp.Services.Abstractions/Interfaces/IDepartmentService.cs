using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetDepartmentsAsync();
    Task<Department?> GetDepartmentAsync(int id);
    Task AddDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
    Task RemoveDepartmentAsync(Department department);
    bool DepartmentExists(string name);
    bool DepartmentExistsInOtherDepartments(int id, string name);
    bool IsDeletable(Department department);
}