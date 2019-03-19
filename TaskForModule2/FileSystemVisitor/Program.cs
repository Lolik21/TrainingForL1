using System;
using System.IO;

namespace FileSystemVisitor
{
    // Only to show functionality !!!!
    internal static class Program
    {
        static void Main(string[] args)
        {
            string rootPath = Environment.SystemDirectory;
            if (args.Length != 0 && Directory.Exists(args[0]))
            {
                rootPath = args[0];
            }
            OperationFactory operationFactory = new OperationFactory();
            Operation operation = operationFactory.CreateOperation();
            operation.Execute(rootPath);
            Console.WriteLine("Press any key to close program...");
            Console.ReadKey();
        }
    }
}
