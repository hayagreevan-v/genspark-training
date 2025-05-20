
namespace EasyQuestions
{
    class EmployeePromotion
    {
        // 1. Create a C# console application which has a class with name “EmployeePromotion” that will take employee names 
        // in the order in which they are eligible for promotion.  
        static List<String> employeeNames = new List<string>();
        static void fetchEmployeeNames()
        {
            System.Console.WriteLine("Please enter the employee names in the order of their eligibility for promotion (Please enter blank to stop) : ");
            string? name = Console.ReadLine();
            while (!string.IsNullOrEmpty(name))
            {
                employeeNames.Add(name);
                name = Console.ReadLine();
            }
        }

        // Given an employee name find his position in the promotion list 
        static int FindEmployeePromotionPosition(string? name)
        {
            return employeeNames.FindIndex(n => n == name);
        }
        static void EmployeePromotionPosition()
        {
            Console.WriteLine("Enter employee's name to find Promotional Postition : ");
            string? name = Console.ReadLine();
            int p = FindEmployeePromotionPosition(name);
            Console.WriteLine($"The Promotional Postion of {name} is {p + 1}");
        }

        // 3. The application seems to be using some excess memory for storing the name, contain the space by using only the quantity of memory that is required. 
        static void OptimizeMemory()
        {
            employeeNames.TrimExcess();
            System.Console.WriteLine("Memery is optimized!");
        }
        // 4. The need for the list is over as all the employees are promoted. Not print all the employee names in ascending order. 
        static void DisplayPromotedEmployees()
        {
            employeeNames.Sort();
            System.Console.WriteLine("Promoted Employees List : ");
            foreach (string n in employeeNames)
            {
                System.Console.WriteLine(n);
            }
        }
        public static void EasyQ()
        {
            fetchEmployeeNames();
            EmployeePromotionPosition();
            OptimizeMemory();
            DisplayPromotedEmployees();
        }
    }
}