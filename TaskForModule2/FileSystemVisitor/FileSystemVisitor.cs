using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSystemVisitor
{
    public class FileSystemVisitor : IFileSystemVisitor
    {
        private readonly Func<DirectoryInfo, bool> _directoryFilter;
        private readonly Func<FileInfo, bool> _fileFilter;
        private readonly IFileSystem _fileSystem;

        public FileSystemVisitor(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            _fileSystem.ErrorOccurred += OnErrorOccurred;
        }

        public FileSystemVisitor(
            IFileSystem fileSystem,
            Func<FileInfo, bool> fileFilter,
            Func<DirectoryInfo, bool> directoryFilter)
            : this(fileSystem)
        {
            _fileFilter = fileFilter;
            _directoryFilter = directoryFilter;
        }

        public event EventHandler<VisitorEventArgs> VisitingFinished;
        public event EventHandler<VisitorEventArgs> VisitingStarted;
        public event EventHandler<VisitorProgressEventArgs> FileFounded;
        public event EventHandler<VisitorProgressEventArgs> DirectoryFounded;
        public event EventHandler<VisitorProgressEventArgs> FilteredFileFounded;
        public event EventHandler<VisitorProgressEventArgs> FilteredDirectoryFounded;
        public event EventHandler<VisitorProgressEventArgs> ErrorOccurred;

        public FileSystemInfo[] Visit(string rootFolder)
        {
            if (!_fileSystem.DirectoryExists(rootFolder))
            {
                throw new ArgumentException(nameof(rootFolder));
            }

            DirectoryInfo directoryInfo = _fileSystem.ProvideDirectoryInfo(rootFolder);
            VisitingStarted?.Invoke(this, new VisitorEventArgs { Message = $"Staring visiting. Folder: {rootFolder}" });
            FileSystemInfo[] fileSystemEntities = Array.Empty<FileSystemInfo>();
            try
            {
                fileSystemEntities = VisitInternal(directoryInfo).ToArray();
            }
            catch (VisitingAbortedException e)
            {
                OnErrorOccurred(this, new ErrorEventArgs(e));
            }
            VisitingFinished?.Invoke(this, new VisitorEventArgs{ Message = "Visiting finished."});
            return fileSystemEntities;
        }

        private IEnumerable<FileSystemInfo> VisitInternal(DirectoryInfo directoryInfo)
        {
            var files = _fileSystem.GetFiles(directoryInfo);
            if (files == null)
            {
                yield break;
            }
            foreach (var file in files)
            {
                if (IsContinueProgress(file, FileFounded) && IsPassedFileFilter(file))
                {
                    yield return file;
                }
            }

            var directories = _fileSystem.GetDirectories(directoryInfo);
            foreach (var directory in directories)
            {
                if (IsContinueProgress(directory, DirectoryFounded) && IsPassedDirectoryFilter(directory))
                {
                    foreach (var fileSystemInfo in VisitInternal(directory))
                    {
                        yield return fileSystemInfo;
                    }
                }
            }
        }

        private void OnErrorOccurred(object sender, ErrorEventArgs e)
        {
            ErrorOccurred?.Invoke(sender, new VisitorProgressEventArgs { Message = e.GetException().Message });
        }

        private bool IsPassedDirectoryFilter(DirectoryInfo directory)
        {
            var isPassed = _fileFilter == null || _directoryFilter.Invoke(directory);
            return isPassed && IsContinueProgress(directory, FilteredDirectoryFounded);
        }

        private bool IsPassedFileFilter(FileInfo file)
        {
            var isPassed = _fileFilter == null || _fileFilter.Invoke(file);
            return isPassed && IsContinueProgress(file, FilteredFileFounded);
        }

        private bool IsContinueProgress(FileSystemInfo entityInfo, EventHandler<VisitorProgressEventArgs> eventHandler)
        {
            var args = new VisitorProgressEventArgs { Message = entityInfo.FullName };
            eventHandler?.Invoke(this, args);
            if (args.IsSkipVisitedEntity)
            {
                return false;
            }

            if (args.IsStopVisiting)
            {
                throw new VisitingAbortedException("Visiting is aborted.");
            }

            return true;
        }
    }
}