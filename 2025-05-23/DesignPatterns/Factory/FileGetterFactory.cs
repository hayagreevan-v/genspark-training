using DesignPatterns.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Factory
{
    public class FileGetterFactory
    {
        public static IFileGetter getFactory(int type)
        {
            if(type == 1)
            {
                return new FileGetter1();
            }
            else if(type == 2)
            {
                return new FileGetter2();
            }
            return null;
        }
    }
}
