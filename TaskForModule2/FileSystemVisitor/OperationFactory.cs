using FileSystemVisitor.FileSystem;

namespace FileSystemVisitor
{
    public class OperationFactory
    {
        public Operation CreateOperation()
        {
            ILogger logger = new ConsoleLogger();
            IFileSystem fileSystem = new FileSystem.FileSystem();
            IFileSystemVisitor visitor = new FileSystemVisitor(fileSystem,
                info => info.Name.Contains("a"),
                info => info.Name.Contains("a"));
            Subscriber subscriber = new Subscriber(visitor, logger);
            return new Operation(visitor, logger) { Subscriber = subscriber };
        }
    }
}