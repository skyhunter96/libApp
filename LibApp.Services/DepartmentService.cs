using Domain.Models;
using EfDataAccess;
using LibApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Services
{
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

        public Task<Department> GetDepartmentAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }

        public Task RemoveDepartmentAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}