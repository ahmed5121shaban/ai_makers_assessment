using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Core.Interfaces;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.ViewModels;

namespace EmployeeManagement.Web.Controllers;

public class HomeController : Controller
{
    private readonly IEmployeeService _employeeService;
    private readonly IDepartmentService _departmentService;

    public HomeController(IEmployeeService employeeService, IDepartmentService departmentService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
    }

    public async Task<IActionResult> Index()
    {
        var totalEmployees = await _employeeService.GetTotalCountAsync();
        var activeEmployees = await _employeeService.GetActiveCountAsync();
        var totalDepartments = await _departmentService.GetCountAsync();

        var viewModel = new DashboardViewModel
        {
            TotalEmployees = totalEmployees,
            ActiveEmployees = activeEmployees,
            InactiveEmployees = totalEmployees - activeEmployees,
            TotalDepartments = totalDepartments
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult NotFoundPage()
    {
        return View();
    }
}
