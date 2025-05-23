using DesignPatterns.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class ReadWrite
    {
        public string Read(IFile file)
        {
            if (file != null) {

                string? fp = file.getFilePath();
                if (fp != null && File.Exists(fp))
                {
                    return File.ReadAllText(fp);
                }
            }
            return string.Empty;
        }

        public bool Write(IFile file, string data) {
            if (file != null)
            {
                string? fp = file.getFilePath();
                if (fp != null)
                {
                    try
                    {
                        File.WriteAllText(fp, data);
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            return false;
        }
    }
}
