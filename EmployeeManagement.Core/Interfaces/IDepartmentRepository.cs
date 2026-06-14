using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Core.Interfaces;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetAllAsync();
    Task<int> GetCountAsync();
    Task<Department?> GetByIdAsync(int id);
    Task<bool> HasEmployeesAsync(int id);
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task DeleteAsync(Department department);
    Task SaveChangesAsync();
}
