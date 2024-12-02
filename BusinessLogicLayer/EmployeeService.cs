using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        //Cant be more than 1,000,000       
        private int totalSalaries = 0;
        //Cant be more than 5
        private int totalEngineersCount = 0;
        //cant be more than 5 
        private int totalManagersCount = 0;
        //cant be more than 1
        private int totalHrCount = 0;
        

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        //Implement the method from the interface, and here I can add logic to whichever methods I want 
        //to add logic to
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
           
            return await _employeeRepository.GetEmployeesAsync();
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task AddEmployeeAsync(Employee employee)
        {
            await CheckPositionCount();
            //Writing logic
            if (employee.Salary <= 10000 && totalSalaries < 1000000)

            {
                if ((employee.Position == "HR" && totalHrCount < 1) ||
                    (employee.Position == "Manager" && totalManagersCount < 5) ||
                    (employee.Position == "Engineer" && totalEngineersCount < 5))
                {
                    totalSalaries += employee.Salary;
                    await _employeeRepository.AddEmployeeAsync(employee);
                }
            }
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        //going to the DAL, and getting the employee from the employees DbSet
        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task GetDetailsAsync(Employee employee)
        {
            await _employeeRepository.GetDetailsAsync(employee);
        }

        //This employee being passed in is coming from my controller
        //...and I am writing logic on this employee before passing it to my DAL
        //..where the changes are being executed on my DbSet which goes to the Database
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            int UpdateTotalSalaries = 0;
        //Cant be more than 5
            int UpdateTotalEngineersCount = 0;
        //cant be more than 5 
            int UpdateTotalManagersCount = 0;
        //cant be more than 1
            int UpdateTotalHrCount = 0;

        //Not updated
            IEnumerable<Employee> employees = await _employeeRepository.GetEmployeesAsync(); 
            List<Employee> employeeList = employees.ToList();
            await CheckPositionCount();

            if (employee.Salary <= 100000)
            {
                for (int i = 0; i < employeeList.Count(); i++)
                {
                    if (employeeList.ElementAt(i).Id == employee.Id)
                    {
                        employeeList.RemoveAt(i);
                        employeeList.Add(employee);
                        break;
                    }
                }

                for (int i = 0; i < employeeList.Count(); i++)
                {
                    UpdateTotalSalaries += employeeList.ElementAt(i).Salary;
                    if (UpdateTotalSalaries > 1000000)
                    {
                        return;
                    }
                    switch (employeeList.ElementAt(i).Position)
                    {
                        case "Manager":
                            UpdateTotalManagersCount++;
                            break;
                        case "Engineer":
                            UpdateTotalEngineersCount++;
                            break;
                        case "HR":
                            UpdateTotalHrCount++;
                            break;
                        default:
                            break;
                    }

                    if (UpdateTotalEngineersCount > 5 || UpdateTotalManagersCount > 5 || UpdateTotalHrCount > 1)
                    {
                        return;
                    }
                }
                await _employeeRepository.UpdateEmployeeAsync(employee);
            }
        }
    

        private async Task CheckPositionCount()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetEmployeesAsync(); 
            //Cant be more than 1,000,000
            totalSalaries = 0;
            //Cant be more than 5
            totalEngineersCount = 0;
            //cant be more than 5
            totalManagersCount = 0;
            //cant be more than 1
            totalHrCount = 0;
            

            for (int i = 0; i < employees.Count(); i++)
            {
                totalSalaries += employees.ElementAt(i).Salary;

                switch (employees.ElementAt(i).Position)
                {
                    case "Manager":
                        totalManagersCount++;
                        break;
                    case "Engineer":
                        totalEngineersCount++;
                        break;
                    case "HR":
                        totalHrCount++;
                        break;
                    default:
                        break;
                }
            }

        }

    }
}
