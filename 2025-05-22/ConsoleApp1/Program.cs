using ConsoleApp1.Misc;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    internal class Program
    {
        delegate void MyDelegate<T>(T n1, T n2);

        void Add(int n1, int n2)
        {
            Console.WriteLine($"{n1} + {n2} = {n1 + n2}");
        }
        void Multiply(int n1, int n2)
        {
            Console.WriteLine($"{n1} * {n2} = {n1 * n2}");
        }
        Program() {
            //MyDelegate<int> del = new MyDelegate<int>(Add);
            //del += Multiply;

            //Predefined Delegate
            Action<int, int> del = Add;
            del += Multiply;
            // Anonymous fn
            del += delegate (int n1, int n2)
            {
                Console.WriteLine($"{n1} - {n2} = {n1 - n2}");
            };
            //lambda fn
            del += (int n1, int n2) =>
            {
                Console.WriteLine($"{n1}/{n2} = {n1 / n2}");
            };
            del(10, 20);
        }


        List<Employee> employees = new List<Employee>()
        {
            new Employee(101,30, "John Doe",  50000),
            new Employee(102, 25,"Jane Smith",  60000),
            new Employee(103,35, "Sam Brown",  70000)
        };
        void FindEmployee() 
        {
            int empId = 102;
            Predicate<Employee> predicate = new Predicate<Employee>((e)=> e.Id == empId);
            Employee? emp = employees.Find(predicate);
            Console.WriteLine(emp);

            // Linq without Extension
            var searchEmployees2 = from employee in employees where employee.Id == empId select employee;
            //Linq with Extension
            var searchEmployees3 = employees.Where(e => e.Id == empId);

        }

        void SortEmployee()
        {
            var sortEmployees = employees.OrderBy(e => e.Name);
            foreach (Employee e in sortEmployees)
            {
                Console.WriteLine(e);
            }
        }

        static void Main(string[] args)
        {
            Program p = new Program();
                p.FindEmployee();
                p.SortEmployee();

            //Extension Method
            Console.WriteLine("studen".StringValidation());

            
        }
    }
}
