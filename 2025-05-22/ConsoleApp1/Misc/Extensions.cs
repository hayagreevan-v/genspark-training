using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Misc
{
    public static class Extensions
    {
        public static bool StringValidation(this string s)
        {
            if(string.IsNullOrEmpty(s)) return false;
            if(s.StartsWith("s") && s.Length == 6) return true;
            return false;
        }
    }
}
