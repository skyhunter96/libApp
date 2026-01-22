using LibApp.Data.Abstractions.Interfaces;
using LibApp.Domain.Models;
using LibApp.Services.Abstractions.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace LibApp.Services;

public class DepartmentService(IDepartmentRepository departmentRepository) : IDepartmentService
{
    public async Task<IEnumerable<Department>> GetDepartmentsAsync()
    {
        return await departmentRepository.GetAllWithUsersAsync();
    }

    public async Task<Department?> GetDepartmentAsync(int id)
    {
        return await departmentRepository.GetByIdWithUsersAsync(id);
    }

    public async Task AddDepartmentAsync(Department department)
    {
        await departmentRepository.AddAsync(department);
    }

    public async Task UpdateDepartmentAsync(Department department)
    {
        await departmentRepository.UpdateAsync(department);
    }

    public async Task RemoveDepartmentAsync(Department department)
    {
        await departmentRepository.RemoveAsync(department);
    }

    public async Task<bool> DepartmentExistsAsync(string name)
    {
        if (name.IsNullOrEmpty())
            return false;

        var departments = await departmentRepository.GetAllAsync();
        var exists = departments.Any(department => string.Equals(department.Name, name, StringComparison.OrdinalIgnoreCase));
        return exists;
    }

    public async Task<bool> DepartmentExistsInOtherDepartmentsAsync(int id, string name)
    {
        if (id == 0 || string.IsNullOrWhiteSpace(name))
            return false;

        var departments = await departmentRepository.GetAllAsync();
        var exists = departments.Any(department => department.Id != id && string.Equals(department.Name, name, StringComparison.OrdinalIgnoreCase));
        return exists;
    }

    public async Task<bool> IsDeletableAsync(int id)
    {
        if (id == 0) return false;

        var departments = await departmentRepository.GetAllAsync();
        return departments.All(department => department.Id == id);
    }
}