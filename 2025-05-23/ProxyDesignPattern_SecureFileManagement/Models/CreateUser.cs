using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDesignPattern_SecureFileManagement.Models
{
    public class CreateUser
    {
        public static User Create()
        {
            User user = new User();
            int r = 0;
            Console.WriteLine("Enter name : ");
            user.Name = Console.ReadLine()??"";
            Console.WriteLine("Enter Role : 1.Admin 2.User 3.Guest ");
            while (!int.TryParse(Console.ReadLine(), out r) || r > 3 || r < 1)
            {
                Console.WriteLine("Invalid Role!");
                Console.WriteLine("Enter Role : 1.Admin 2.User 3.Guest ");
            }
            switch (r)
            {
                case 1:
                    user.UserRole = Role.Admin; break;
                case 2:
                    user.UserRole = Role.User; break;
                case 3:
                    user.UserRole = Role.Guest; break;
                default:
                    Console.WriteLine("Invalid Input");
                    break;
            }

            return user;
        }
    }
}
