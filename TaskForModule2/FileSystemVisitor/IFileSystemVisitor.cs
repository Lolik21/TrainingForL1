using System;
using System.IO;
using FileSystemVisitor.EventArgs;

namespace FileSystemVisitor
{
    public interface IFileSystemVisitor
    {
        FileSystemInfo[] Visit(string rootFolder);
        event EventHandler<VisitorEventArgs> VisitingFinished;
        event EventHandler<VisitorEventArgs> VisitingStarted;
        event EventHandler<VisitorProgressEventArgs> FileFounded;
        event EventHandler<VisitorProgressEventArgs> DirectoryFounded;
        event EventHandler<VisitorProgressEventArgs> FilteredFileFounded;
        event EventHandler<VisitorProgressEventArgs> FilteredDirectoryFounded;
        event EventHandler<VisitorProgressEventArgs> ErrorOccurred;
    }
}