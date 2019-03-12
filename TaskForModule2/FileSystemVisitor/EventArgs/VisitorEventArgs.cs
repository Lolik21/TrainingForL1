using System;

namespace FileSystemVisitor
{
    public class VisitorEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}