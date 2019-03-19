namespace FileSystemVisitor.EventArgs
{
    public sealed class VisitorProgressEventArgs : VisitorEventArgs
    {
        public bool IsStopVisiting { get; set; }
        public bool IsSkipVisitedEntity { get; set; }
    }
}