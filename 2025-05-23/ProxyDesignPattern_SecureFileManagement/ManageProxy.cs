using ProxyDesignPattern_SecureFileManagement.Interfaces;
using ProxyDesignPattern_SecureFileManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDesignPattern_SecureFileManagement
{
    public class ManageProxy
    {
        ProxyFile _proxyFile;

        public ManageProxy(IFile proxyFile)
        {
            _proxyFile = (ProxyFile) proxyFile;
        }
        

        public void Start()
        {
            Console.WriteLine("Login : ");
            User user = CreateUser.Create();
            string data = _proxyFile.Read(user);
            Console.WriteLine(data);
        }
    }
}
