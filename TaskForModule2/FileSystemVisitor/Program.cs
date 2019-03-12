using System;
using System.IO;

namespace FileSystemVisitor
{
    // Only to show functionality
    internal static class Program
    {
        static void Main(string[] args)
        {
            string rootPath = Environment.SystemDirectory;
            if (args.Length != 0 && Directory.Exists(args[0]))
            {
                rootPath = args[0];
            }
            IFileSystem fileSystem = new FileSystem();
            IFileSystemVisitor visitor = new FileSystemVisitor(fileSystem,
                info => info.Name.Contains("a"),
                info => info.Name.Contains("a"));
            visitor.VisitingStarted += OnVisitingStarted;
            visitor.FileFounded += OnFileFounded;
            visitor.ErrorOccurred += OnErrorOccurred;
            visitor.DirectoryFounded += OnDirectoryFounded;
            visitor.FilteredDirectoryFounded += OnFilteredDirectoryFounded;
            visitor.FilteredFileFounded += OnFilteredFileFounded;
            visitor.VisitingFinished += OnVisitingFinished;
            visitor.Visit(rootPath);
            Console.WriteLine("Press any key to close program...");
            Console.ReadKey();
        }

        private static void OnVisitingFinished(object sender, VisitorEventArgs e)
        {
            WriteWithColor(ConsoleColor.Red, e.Message);
        }

        private static void OnDirectoryFounded(object sender, VisitorEventArgs e)
        {
            WriteWithColor(ConsoleColor.Blue, e.Message);
        }

        private static void OnErrorOccurred(object sender, VisitorEventArgs e)
        {
            WriteWithColor(ConsoleColor.Red, e.Message);
        }

        private static void OnFileFounded(object sender, VisitorEventArgs e)
        {
            WriteWithColor(ConsoleColor.Green, e.Message);
        }

        private static void OnVisitingStarted(object sender, VisitorEventArgs e)
        {
            WriteWithColor(ConsoleColor.Blue, e.Message);
        }

        private static void OnFilteredFileFounded(object sender, VisitorEventArgs e)
        {
            WriteWithColor(ConsoleColor.Yellow, e.Message);
        }

        private static void OnFilteredDirectoryFounded(object sender, VisitorEventArgs e)
        {
            WriteWithColor(ConsoleColor.Gray, e.Message);
        }

        private static void WriteWithColor(ConsoleColor consoleColor, string message)
        {
            var savedColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = savedColor;
        }
    }
}
