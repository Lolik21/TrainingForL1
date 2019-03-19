using System;

namespace FileSystemVisitor
{
    public class Operation
    {
        private readonly IFileSystemVisitor _fileSystemVisitor;
        private readonly ILogger _logger;

        public Operation(IFileSystemVisitor fileSystemVisitor, ILogger logger)
        {
            _fileSystemVisitor = fileSystemVisitor;
            _logger = logger;
        }

        public Subscriber Subscriber { get; set; }

        public void Execute(string rootFolder)
        {
            try
            {
                _fileSystemVisitor.Visit(rootFolder);
            }
            catch (ArgumentException exception)
            {
                _logger.Log(exception.Message);
            }
        }
    }
}