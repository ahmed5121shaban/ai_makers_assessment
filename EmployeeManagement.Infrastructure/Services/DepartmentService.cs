using Microsoft.Extensions.Logging;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;

namespace EmployeeManagement.Infrastructure.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(IDepartmentRepository repository, ILogger<DepartmentService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _repository.GetCountAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Department department)
    {
        await _repository.AddAsync(department);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Department '{Name}' created (ID {Id})", department.Name, department.DepartmentId);
    }

    public async Task UpdateAsync(Department department)
    {
        await _repository.UpdateAsync(department);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Department '{Name}' updated (ID {Id})", department.Name, department.DepartmentId);
    }

    public async Task<(bool Success, string Message)> DeleteAsync(int id)
    {
        var department = await _repository.GetByIdAsync(id);
        if (department == null)
            return (false, "Department not found.");

        if (await _repository.HasEmployeesAsync(id))
            return (false, "Cannot delete this department because it has employees linked to it.");

        await _repository.DeleteAsync(department);
        await _repository.SaveChangesAsync();
        _logger.LogInformation("Department '{Name}' deleted (ID {Id})", department.Name, id);
        return (true, "Department deleted successfully.");
    }
}
