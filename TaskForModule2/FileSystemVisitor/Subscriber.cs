using FileSystemVisitor.EventArgs;

namespace FileSystemVisitor
{
    public class Subscriber
    {
        private readonly ILogger _logger;

        public Subscriber(IFileSystemVisitor fileSystemVisitor, ILogger logger)
        {
            fileSystemVisitor.VisitingStarted += OnVisitorEvent;
            fileSystemVisitor.FileFounded += OnVisitorEvent;
            fileSystemVisitor.ErrorOccurred += OnVisitorEvent;
            fileSystemVisitor.DirectoryFounded += OnVisitorEvent;
            fileSystemVisitor.FilteredDirectoryFounded += OnVisitorEvent;
            fileSystemVisitor.FilteredFileFounded += OnVisitorEvent;
            fileSystemVisitor.VisitingFinished += OnVisitorEvent;
            _logger = logger;
        }

        private void OnVisitorEvent(object sender, VisitorEventArgs e)
        {
            _logger.Log(e.Message);
        }
    }
}