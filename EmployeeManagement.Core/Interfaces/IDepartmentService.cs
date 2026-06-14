using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Core.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<int> GetCountAsync();
    Task<Department?> GetByIdAsync(int id);
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task<(bool Success, string Message)> DeleteAsync(int id);
}
