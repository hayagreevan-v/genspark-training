using DesignPatterns.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SingleTon
{
    public class File2 : ISingleton, IFile
    {
        static File2 file;
        string Filepath { get; } = "file2.txt";
        private File2(){}
        public static File2 getInstance()
        {
            if(file== null)
            {
                file = new File2();
            }
            return file;
        }

        public string getFilePath()
        {
            return file.Filepath;
        }
    }
}
