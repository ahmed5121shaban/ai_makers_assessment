using EmployeeManagement.Core.Entities;

namespace EmployeeManagement.Core.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync(string? search = null, int page = 1, int pageSize = 10);
    Task<int> GetCountAsync(string? search = null);
    Task<int> GetTotalCountAsync();
    Task<int> GetActiveCountAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<bool> EmailExistsAsync(string email, int? excludeId = null);
    Task AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task SaveChangesAsync();
}
