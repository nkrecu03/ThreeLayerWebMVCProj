using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using ThreeLayerIntro.Models;

namespace ThreeLayerIntro.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        //For my index view, I will call the GetEmployeesAsync method to populate my employees variable
        //and this GetEmployeesAsync method is coming from my IEmployeeService inside my BLL
        public async Task<IActionResult> Index()
        {
            var employees = await employeeService.GetEmployeesAsync();
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeModel employeeModel)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    FirstName = employeeModel.FirstName,
                    LastName = employeeModel.LastName,
                    Position = employeeModel.Position,
                    Salary = employeeModel.Salary
                };
                await employeeService.AddEmployeeAsync(employee);
                return RedirectToAction("Index");
            }
            return View(employeeModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            await employeeService.GetDetailsAsync(employee);
            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee editedEmployee)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    //Id must be set here so we know what employee to update
                    Id = editedEmployee.Id,
                    FirstName = editedEmployee.FirstName,
                    LastName = editedEmployee.LastName,
                    Position = editedEmployee.Position,
                    Salary = editedEmployee.Salary
                };
                await employeeService.UpdateEmployeeAsync(employee);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editedEmployee);
            }
        }
    }
}
