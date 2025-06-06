using System;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Models;

namespace ConsoleApp1.Services
{
    
    public class EmployeeService : IEmployeeService
    {
        IRepository<int, Employee> _employeeRepository;
        public EmployeeService(IRepository<int, Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int AddEmployee(Employee employee)
        {
            try
            {
                var result = _employeeRepository.Add(employee);
                if (result != null) return result.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public List<Employee>? SearchEmployee(SearchModel searchModel)
        {
            try
            {
                var employees = _employeeRepository.GetAll();
                employees = SearchById(employees, searchModel.Id);
                employees = SearchByName(employees, searchModel.Name);
                employees = SearchByAge(employees, searchModel.Age);
                employees = SearchBySalary(employees, searchModel.Salary);
                if (employees != null && employees.Count > 0)
                    return employees.ToList();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            return null;
        }


        private ICollection<Employee> SearchBySalary(ICollection<Employee> employees, Range<double>? salary) {
            if (salary == null ||  employees.Count == 0 ) return employees;
            return employees.Where(e => e.Salary >= salary.MinVal && e.Salary <= salary.MaxVal).ToList();
        }
        private ICollection<Employee> SearchByAge(ICollection<Employee> employees, Range<int>? age) {
            if (age == null || employees.Count == 0 ) return employees;
            return employees.Where(e => e.Age >= age.MinVal && e.Age <= age.MaxVal).ToList();
        }
        private ICollection<Employee> SearchByName(ICollection<Employee> employees, string? name) {
            if (name == null || employees.Count == 0 ) return employees;
            return employees.Where(e => e.Name.ToLower().Contains(name.ToLower())).ToList();
        }
        private ICollection<Employee> SearchById(ICollection<Employee> employees, int? id) {
            if (id == null | employees.Count() == 0 ) return employees;
            return employees.Where(e => e.Id == id).ToList();
        }
    }
}
