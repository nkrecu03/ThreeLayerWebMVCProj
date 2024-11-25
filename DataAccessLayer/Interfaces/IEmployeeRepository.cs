using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IEmployeeRepository
    {
        //Creating GetEmployeesAsync inside my interface to be implemented
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        //Creating AddEmployeeAsync method to be implemented inside my EmployeeRepository class
        Task AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task GetDetailsAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
    }
}
