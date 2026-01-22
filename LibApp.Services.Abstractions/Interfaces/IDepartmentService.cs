using LibApp.Domain.Models;

namespace LibApp.Services.Abstractions.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetDepartmentsAsync();
    Task<Department?> GetDepartmentAsync(int id);
    Task AddDepartmentAsync(Department department);
    Task UpdateDepartmentAsync(Department department);
    Task RemoveDepartmentAsync(Department department);
    Task<bool> DepartmentExistsAsync(string name);
    Task<bool> DepartmentExistsInOtherDepartmentsAsync(int id, string name);
    Task<bool> IsDeletableAsync(int id);
}