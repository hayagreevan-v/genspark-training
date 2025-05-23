using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDesignPattern_SecureFileManagement.Models
{
    public enum Role { Admin, User, Guest};
    public class User
    {
        public string Name { get; set; } = string.Empty;
        public Role UserRole { get; set; }

    }
}
