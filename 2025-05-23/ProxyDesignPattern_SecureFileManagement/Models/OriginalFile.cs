using ProxyDesignPattern_SecureFileManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDesignPattern_SecureFileManagement.Models
{
    public class OriginalFile : IFile
    {
        public string Filepath { get; } = "secured.txt";
        public string Read(User user)
        {
            return File.ReadAllText(Filepath);
        }
    }
}
