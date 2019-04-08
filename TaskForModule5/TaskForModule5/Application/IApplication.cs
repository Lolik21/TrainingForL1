using System;
using System.Collections.Generic;
using TaskForModule5.FileBroker;

namespace TaskForModule5.Application
{
    public interface IApplication : IDisposable
    {
        void Run();
        List<IFileBroker> FileBrokers { get; set; }
    }
}