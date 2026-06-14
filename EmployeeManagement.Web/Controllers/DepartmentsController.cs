using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Core.Entities;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Web.ViewModels;

namespace EmployeeManagement.Web.Controllers;

public class DepartmentsController : Controller
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await _departmentService.GetAllAsync();
        var viewModels = departments.Select(d => new DepartmentViewModel
        {
            DepartmentId = d.DepartmentId,
            Name = d.Name,
            Description = d.Description,
            EmployeeCount = d.Employees?.Count ?? 0
        });

        return View(viewModels);
    }

    public IActionResult Create()
    {
        return View(new DepartmentViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DepartmentViewModel viewModel)
    {
        if (!ModelState.IsValid) return View(viewModel);

        var department = new Department
        {
            Name = viewModel.Name,
            Description = viewModel.Description
        };

        await _departmentService.AddAsync(department);
        TempData["SuccessMessage"] = $"Department '{department.Name}' created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var department = await _departmentService.GetByIdAsync(id);
        if (department == null) return NotFound();

        return View(new DepartmentViewModel
        {
            DepartmentId = department.DepartmentId,
            Name = department.Name,
            Description = department.Description
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, DepartmentViewModel viewModel)
    {
        if (id != viewModel.DepartmentId) return BadRequest();
        if (!ModelState.IsValid) return View(viewModel);

        var department = await _departmentService.GetByIdAsync(id);
        if (department == null) return NotFound();

        department.Name = viewModel.Name;
        department.Description = viewModel.Description;

        await _departmentService.UpdateAsync(department);
        TempData["SuccessMessage"] = $"Department '{department.Name}' updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var (success, message) = await _departmentService.DeleteAsync(id);

        if (success)
            TempData["SuccessMessage"] = message;
        else
            TempData["ErrorMessage"] = message;

        return RedirectToAction(nameof(Index));
    }
}
