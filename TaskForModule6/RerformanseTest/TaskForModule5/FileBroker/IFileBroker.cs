using System;

namespace TaskForModule5.FileBroker
{
    public interface IFileBroker : IDisposable
    {
        void AttachToFolder(string folderPath);
    }
}