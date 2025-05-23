
using DesignPatterns.Factory;
using DesignPatterns.Interface;

namespace DesignPatterns
{
    internal class Program
    {

        static void Main(string[] args)
        {
            IFileGetter fileGetter = FileGetterFactory.getFactory(1);
            IFile file = fileGetter.getFile();

            Console.WriteLine(file);

            ReadWrite readWrite = new ReadWrite();
            string? context = readWrite.Read(file);
            Console.WriteLine(context??"");

            bool f = readWrite.Write(file, "Hii");
            Console.WriteLine(f);

        }
    }
}
