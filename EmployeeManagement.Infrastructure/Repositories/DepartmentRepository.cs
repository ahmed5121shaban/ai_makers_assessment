using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Infrastructure.Data;

namespace EmployeeManagement.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly AppDbContext _context;

    public DepartmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _context.Departments.OrderBy(d => d.Name).ToListAsync();
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Departments.CountAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments.FindAsync(id);
    }

    public async Task<bool> HasEmployeesAsync(int id)
    {
        return await _context.Employees.AnyAsync(e => e.DepartmentId == id);
    }

    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
    }

    public Task UpdateAsync(Department department)
    {
        _context.Departments.Update(department);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Department department)
    {
        _context.Departments.Remove(department);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
