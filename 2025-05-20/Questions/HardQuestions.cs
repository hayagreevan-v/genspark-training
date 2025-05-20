using System;
using System.ComponentModel.Design;
using System.Xml.Serialization;
namespace MediumHardQuestions
{
    public class HardQuestions
    {
        static Dictionary<int, Employee> employeeDict = new Dictionary<int, Employee>();
        static void displayAll()
        {
            if (employeeDict.Count() == 0)
            {
                System.Console.WriteLine("No employees found!");
                return;
            }
            System.Console.WriteLine("------------All Employees--------------");
            foreach (KeyValuePair<int, Employee> kv in employeeDict)
            {
                Console.WriteLine(kv.Value);
                Console.WriteLine();
            }
        }
        static void createEmployee()
        {
            Employee e = new Employee();
            e.TakeEmployeeDetailsFromUser();
            bool f1 = true;
            int v;
            while (!f1 || employeeDict.ContainsKey(e.Id))
            {
                Console.Write("Employee Id Already Exists or Incorrect Input\nProvide a new Employee Id : ");
                f1 = int.TryParse(Console.ReadLine(), out v);
                e.Id = v;
            }
            employeeDict.Add(e.Id, e);
        }

        static void modifyEmployee()
        {
            int id;
            System.Console.WriteLine("Enter Employee ID to modify : ");
            bool f = int.TryParse(Console.ReadLine(), out id);
            if (!f)
            {
                System.Console.WriteLine("Invalid Input");
                return;
            }
            if (!employeeDict.ContainsKey(id))
            {
                System.Console.WriteLine("No employee found");
                return;
            }
            Employee emp = employeeDict[id];
            string? s;

            Console.WriteLine($"Please enter the employee name [{emp.Name}]");
            s = Console.ReadLine();
            if (!string.IsNullOrEmpty(s))
                emp.Name = s;

            Console.WriteLine($"Please enter the employee age [{emp.Age}]");
            s = Console.ReadLine();
            if (!string.IsNullOrEmpty(s)) 
                emp.Age = Convert.ToInt32(s);

            Console.WriteLine($"Please enter the employee salary [{emp.Salary}]");
            s = Console.ReadLine();
            if (!string.IsNullOrEmpty(s)) 
                emp.Salary = Convert.ToDouble(s);
        }

        static void display()
        {
            int id;
            System.Console.WriteLine("Enter Employee ID to display : ");
            bool f = int.TryParse(Console.ReadLine(), out id);
            if (!f)
            {
                System.Console.WriteLine("Invalid Input");
                return;
            }
            if (!employeeDict.ContainsKey(id))
            {
                System.Console.WriteLine("No employee found");
                return;
            }
            System.Console.WriteLine(employeeDict[id]);
        }
        static void deleteEmployee()
        {
            int id;
            System.Console.WriteLine("Enter Employee ID to delete : ");
            bool f = int.TryParse(Console.ReadLine(), out id);
            if (!f)
            {
                System.Console.WriteLine("Invalid Input");
                return;
            }
            if (!employeeDict.ContainsKey(id))
            {
                System.Console.WriteLine("No data found to delete");
                return;
            }
            employeeDict.Remove(id);
            Console.WriteLine($"Deletion of ID : {id} success");
        }
        public static void HardQ1()
        {
            int choice = 0;
            while (choice != 6)
            {
                Console.WriteLine("1. Display all Employees");
                Console.WriteLine("2. Add Employee");
                Console.WriteLine("3. Modify Employee");
                Console.WriteLine("4. Display an Employee by ID");
                Console.WriteLine("5. Delete an Employee");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your Choice : ");

                bool flag = int.TryParse(Console.ReadLine(), out choice);
                while (!flag || choice > 6 || choice < 1)
                {
                    Console.WriteLine("Invalid Selection.");
                    Console.Write("Enter your Choice : ");
                    flag = int.TryParse(Console.ReadLine(), out choice);
                }

                switch (choice)
                {
                    case 1:
                        displayAll();
                        break;
                    case 2:
                        createEmployee();
                        break;
                    case 3:
                        modifyEmployee();
                        break;
                    case 4 :
                        display();
                        break;
                    case 5:
                        deleteEmployee();
                        break;
                    case 6:
                        System.Console.WriteLine("Exiting...");
                        return;
                    default:
                        System.Console.WriteLine("Invalid Input");
                        break;
                }
                System.Console.WriteLine();
                System.Console.WriteLine();
            }
        }
    }
    
}

