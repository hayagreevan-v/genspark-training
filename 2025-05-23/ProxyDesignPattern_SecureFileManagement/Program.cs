using ProxyDesignPattern_SecureFileManagement.Interfaces;
using ProxyDesignPattern_SecureFileManagement.Models;

namespace ProxyDesignPattern_SecureFileManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IFile original_file = new OriginalFile();
            Logger logger = Logger.getInstance();
            IFile proxy_file  = new ProxyFile(original_file, logger);
            ManageProxy proxy = new ManageProxy(proxy_file);
            proxy.Start();
        }
    }
}
