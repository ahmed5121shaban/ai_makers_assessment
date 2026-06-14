using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Web.ViewModels;

namespace EmployeeManagement.Web.Controllers;

public class EmployeesController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;

    public EmployeesController(IEmployeeService employeeService, IDepartmentService departmentService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index(string? search, int page = 1)
    {
        var (employees, totalCount) = await _employeeService.GetEmployeesAsync(search, page, 10);

        var viewModel = new EmployeeListViewModel
        {
            Employees = employees.Select(MapToViewModel),
            Search = search,
            Page = page,
            TotalCount = totalCount
        };

        return View(viewModel);
    }

    public async Task<IActionResult> Details(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null) return NotFound();

        return View(MapToViewModel(employee));
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Departments = await _departmentService.GetAllAsync();
        return View(new EmployeeViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(EmployeeViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _departmentService.GetAllAsync();
            return View(viewModel);
        }

        try
        {
            var employee = new Employee
            {
                FullName = viewModel.FullName,
                Email = viewModel.Email,
                MobileNumber = viewModel.MobileNumber,
                DepartmentId = viewModel.DepartmentId,
                JobTitle = viewModel.JobTitle,
                HireDate = viewModel.HireDate
            };

            await _employeeService.AddAsync(employee);
            TempData["SuccessMessage"] = $"Employee '{employee.FullName}' created successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.Departments = await _departmentService.GetAllAsync();
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetByIdAsync(id);
        if (employee == null) return NotFound();

        ViewBag.Departments = await _departmentService.GetAllAsync();
        return View(MapToViewModel(employee));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EmployeeViewModel viewModel)
    {
        if (id != viewModel.EmployeeId) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.Departments = await _departmentService.GetAllAsync();
            return View(viewModel);
        }

        try
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null) return NotFound();

            employee.FullName = viewModel.FullName;
            employee.Email = viewModel.Email;
            employee.MobileNumber = viewModel.MobileNumber;
            employee.DepartmentId = viewModel.DepartmentId;
            employee.JobTitle = viewModel.JobTitle;
            employee.HireDate = viewModel.HireDate;

            await _employeeService.UpdateAsync(employee);
            TempData["SuccessMessage"] = $"Employee '{employee.FullName}' updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError("", ex.Message);
            ViewBag.Departments = await _departmentService.GetAllAsync();
            return View(viewModel);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _employeeService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Employee deactivated successfully.";
        }
        catch (KeyNotFoundException)
        {
            TempData["ErrorMessage"] = "Employee not found.";
        }

        return RedirectToAction(nameof(Index));
    }

    private static EmployeeViewModel MapToViewModel(Employee employee)
    {
        return new EmployeeViewModel
        {
            EmployeeId = employee.EmployeeId,
            FullName = employee.FullName,
            Email = employee.Email,
            MobileNumber = employee.MobileNumber,
            DepartmentId = employee.DepartmentId,
            DepartmentName = employee.Department?.Name,
            JobTitle = employee.JobTitle,
            HireDate = employee.HireDate,
            IsActive = employee.IsActive
        };
    }
}
