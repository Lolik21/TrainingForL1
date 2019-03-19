using System;
using System.IO;

namespace FileSystemVisitor.FileSystem
{
    internal class FileSystem : IFileSystem
    {
        public FileInfo[] GetFiles(DirectoryInfo directoryInfo)
        {
            return ExecuteSafe(directoryInfo.GetFiles);
        }

        public DirectoryInfo[] GetDirectories(DirectoryInfo directoryInfo)
        {
            return ExecuteSafe(directoryInfo.GetDirectories);
        }

        public event EventHandler<ErrorEventArgs> ErrorOccurred;
        public bool DirectoryExists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        public DirectoryInfo ProvideDirectoryInfo(string folderPath)
        {
            return new DirectoryInfo(folderPath);
        }

        private T ExecuteSafe<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                ErrorOccurred?.Invoke(this, new ErrorEventArgs(e));
                return default(T);
            }
        }
    }
}