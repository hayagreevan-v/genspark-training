using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using ConsoleApp1.Interfaces;
using ConsoleApp1.Models;

namespace ConsoleApp1;

public class ManageEmployee
{
    private readonly IEmployeeService _employeeService;

    public ManageEmployee(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    void AddEmployee()
    {
        Employee e = new Employee();
        e.TakeEmployeeDetailsFromUser();
        _employeeService.AddEmployee(e);
        System.Console.WriteLine($"Inserted new record with Id : {e.Id}");

    }
    SearchModel GetSearchModel()
    {
        SearchModel searchModel = new SearchModel();
        string? s,s2;
        int id, minAge,maxAge;
        double minSalary, maxSalary;

        System.Console.WriteLine("Enter Search ID [or press Enter]:");
        s = Console.ReadLine();
        if (!string.IsNullOrEmpty(s) && int.TryParse(s, out id))
            searchModel.Id = id;

        System.Console.WriteLine("Enter Search Name [or press Enter]:");
        s = Console.ReadLine();
        if (!string.IsNullOrEmpty(s))
            searchModel.Name = s;

        System.Console.WriteLine("Enter Search Age Range - Min & Max Value [or press Enter]:");
        s = Console.ReadLine();
        s2 = Console.ReadLine();
        if(!string.IsNullOrEmpty(s) && int.TryParse(s, out minAge) && !string.IsNullOrEmpty(s2) && int.TryParse(s2, out maxAge)) {
            searchModel.Age = new Range<int>();
            searchModel.Age.MinVal = minAge;
            searchModel.Age.MaxVal = maxAge;
        }

        System.Console.WriteLine("Enter Search Salary Range - Min & Max Value [or press Enter]:");
        s = Console.ReadLine();
        s2 = Console.ReadLine();
        if(!string.IsNullOrEmpty(s) && double.TryParse(s, out minSalary) && !string.IsNullOrEmpty(s2) && double.TryParse(s2, out maxSalary)) {
            searchModel.Salary = new Range<double>();
            searchModel.Salary.MinVal = minSalary;
            searchModel.Salary.MaxVal = maxSalary;
        }

        return searchModel;
    }
    void printEmployees(List<Employee>? employees)
    {
        if (employees == null || employees.Count() == 0) {
            System.Console.WriteLine("No employees found!");
            return;
        }
        Console.WriteLine("--------Employees----------");
        foreach (Employee e in employees)
        {
            System.Console.WriteLine(e);
            System.Console.WriteLine();
        }
    }
    void searchEmployee()
    {
        SearchModel searchModel = GetSearchModel();
        List<Employee>? employees = _employeeService.SearchEmployee(searchModel);
        printEmployees(employees);
    }
    public void Start()
    {
        int choice = 0;
        while (choice <= 3)
        {
            Console.WriteLine("1. Add Employee");
            Console.WriteLine("2. Search Employee");
            Console.WriteLine("3. Exit");
            Console.WriteLine("Enter your choice : ");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
            {
                Console.WriteLine("Invalid Input!");
                Console.WriteLine("Enter your choice : ");
            }
            switch (choice)
            {
                case 1:
                    AddEmployee();
                    break;
                case 2:
                    searchEmployee();
                    break;
                case 3:
                    System.Console.WriteLine("Exiting...");
                    return;
                default:
                    System.Console.WriteLine("Invalid Input");
                    break;
            }
        }
    }
}
