using ConsoleApp1.Models;
using ConsoleApp1.Repositories;
using ConsoleApp1.Services;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            EmployeeService employeeService = new EmployeeService(employeeRepository);
            ManageEmployee manageEmployee = new ManageEmployee(employeeService);
            manageEmployee.Start();
        }
    }
}
