namespace FileSystemVisitor
{
    public class VisitorProgressEventArgs : VisitorEventArgs
    {
        public bool IsStopVisiting { get; set; }
        public bool IsSkipVisitedEntity { get; set; }
    }
}