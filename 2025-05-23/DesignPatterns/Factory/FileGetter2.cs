using DesignPatterns.Interface;
using DesignPatterns.SingleTon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factory
{
    public class FileGetter2 : IFileGetter
    {
        public IFile getFile()
        {
            return File2.getInstance();
        }
    }
}
