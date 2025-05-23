using DesignPatterns.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SingleTon
{
    public class File1 : ISingleton,IFile
    {
        static File1 file;
        string Filepath { get; } = "file1.txt";
        private File1(){}
        public static File1 getInstance()
        {
            if(file== null)
            {
                file = new File1();
            }
            return file;
        }

        public string getFilePath()
        {
            return file.Filepath;
        }
    }
}
