using DesignPatterns.Interface;
using DesignPatterns.SingleTon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factory
{
    internal class FileGetter1 : IFileGetter
    {
        public IFile getFile()
        {
            return File1.getInstance();
        }
    }
}
