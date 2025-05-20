namespace MediumHardQuestions
{
    class MediumQuestions
    {
        // Q1
        static Dictionary<int, Employee> employeeDict = new Dictionary<int, Employee>();
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
        static Employee getEmployeeById(int id)
        {
            return employeeDict[id];
        }

        public static void MediumQ1()
        {
            System.Console.WriteLine("Enter number of New Employees : ");
            int n = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"For Employee {i + 1} : ");
                createEmployee();
                Console.WriteLine();
            }
            foreach (KeyValuePair<int, Employee> kv in employeeDict)
            {
                System.Console.WriteLine(kv.Key + " " + kv.Value.ToString());
            }

            System.Console.Write("Enter Employee id to find : ");
            int id;
            bool f1 = int.TryParse(Console.ReadLine(), out id);
            while (!f1 || !employeeDict.ContainsKey(id))
            {
                Console.Write("Employee Id doesn't exist or Invalid Input\nEnter Employee Id to find : ");
                f1 = int.TryParse(Console.ReadLine(), out id);

            }

            System.Console.WriteLine(getEmployeeById(id));
        }


        // Q2
        static List<Employee> employeeList = new List<Employee>();
        static void createEmployee_List()
        {
            Employee e = new Employee();
            e.TakeEmployeeDetailsFromUser();
            bool f1 = true;
            int v;
            while (!f1 || employeeList.Count(el => el.Id == e.Id) != 0)
            {
                Console.Write("Employee Id Already Exists or Incorrect Input\nProvide a new Employee Id : ");
                f1 = int.TryParse(Console.ReadLine(), out v);
                e.Id = v;
            }
            employeeList.Add(e);
        }
        static Employee? findEmployeeById_List(int id)
        {
            List<Employee> emp = employeeList.Where(e => e.Id == id).ToList();
            if (emp.Count() == 0) return null;
            return emp[0];
        }
        static void EmployeeSalarySort()
        {
            employeeList.Sort();
        }

        static void PopulateEmployeeList()
        {
            System.Console.WriteLine("Enter number of New Employees : ");
            int n = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"For Employee {i + 1} : ");
                createEmployee_List();
                Console.WriteLine();
            }
        }
        public static void MediumQ2()
        {
            PopulateEmployeeList();
            EmployeeSalarySort();
            foreach (Employee e in employeeList)
            {
                System.Console.WriteLine(e.ToString());
            }


            System.Console.Write("Enter Employee id to find : ");
            int id;
            bool f1 = int.TryParse(Console.ReadLine(), out id);
            while (!f1)
            {
                Console.Write("Invalid Input\nEnter Employee Id to find : ");
                f1 = int.TryParse(Console.ReadLine(), out id);

            }

            Employee? f = findEmployeeById_List(id);
            if (f == null) System.Console.WriteLine("No records found!");
            else System.Console.WriteLine(f);
        }

        static List<Employee>? findEmployeeByName_List(string? name)
        {
            List<Employee> emp = employeeList.Where(e => e.Name == name).ToList();
            if (emp.Count() == 0) return null;
            return emp;
        }

        public static void MediumQ3()
        {
            PopulateEmployeeList();

            System.Console.Write("Enter Employee Name to find : ");
            string? name = Console.ReadLine();

            List<Employee>? list = findEmployeeByName_List(name);
            if (list == null) System.Console.WriteLine("No records found!");
            else
            {
                foreach (Employee e in list)
                    System.Console.WriteLine(e);
                System.Console.WriteLine();
            }
        }

        static List<Employee>? findEmployeeWithGreaterAge(Employee employee)
        {
            List<Employee> emp = employeeList.Where(e => e.Age > employee.Age).ToList();
            if (emp.Count() == 0) return null;
            return emp;
        }
        public static void MediumQ4()
        {
            PopulateEmployeeList();

            System.Console.Write("Enter Employee id to find : ");
            int id;
            bool f1 = int.TryParse(Console.ReadLine(), out id);
            while (!f1)
            {
                Console.Write("Invalid Input\nEnter Employee Id to find elders : ");
                f1 = int.TryParse(Console.ReadLine(), out id);

            }
            Employee? f = findEmployeeById_List(id);
            if (f == null)
            {
                System.Console.WriteLine("No user found!");
                return;
            }
            else
            {
                List<Employee>? list = findEmployeeWithGreaterAge(f);
                if (list == null) System.Console.WriteLine("No elders found!");
                else
                {
                    System.Console.WriteLine("Elders are : ");
                    foreach (Employee e in list)
                    {
                        System.Console.WriteLine(e);
                        System.Console.WriteLine();
                    }
                }
            }
        }


        
    }
}