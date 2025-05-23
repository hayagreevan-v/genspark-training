using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyDesignPattern_SecureFileManagement.Models
{
    public class Logger
    {
        static Logger logger;
        private Logger() { }
        private static readonly string FilePath = "log.txt";
        

        public static Logger getInstance()
        {
            if (logger == null)
            {
                logger = new Logger();
            }
            return logger;
        }
        public void Log(string message)
        {
            Console.WriteLine(message);
            File.AppendAllText(FilePath, message+"\n");
        }
    }
}
