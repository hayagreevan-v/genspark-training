using ProxyDesignPattern_SecureFileManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDesignPattern_SecureFileManagement.Interfaces
{
    public interface IFile
    {
        public string Filepath { get; }
        string Read(User user);
    }
}
