using System;
using System.IO;

namespace FileSystemVisitor
{
    public interface IFileSystem
    {
        FileInfo[] GetFiles(DirectoryInfo directoryInfo);
        DirectoryInfo[] GetDirectories(DirectoryInfo directoryInfo);
        event EventHandler<ErrorEventArgs> ErrorOccurred;
        bool DirectoryExists(string folderPath);
        DirectoryInfo ProvideDirectoryInfo(string folderPath);
    }
}