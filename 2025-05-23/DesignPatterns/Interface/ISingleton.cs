using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Interface
{
    public interface ISingleton
    {
        //static T file;
        static virtual IFile getInstance()
        {
            return null;
        }
    }
}
