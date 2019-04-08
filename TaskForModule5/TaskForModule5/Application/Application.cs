using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TaskForModule5.FileBroker;
using TaskForModule5.LocaleSelectors;
using TaskForModule5.Settings;
using Unity;

namespace TaskForModule5.Application
{
    public class Application : IApplication
    {
        private readonly ILocaleSelector _localeSelector;
        private readonly IEnumerable<string> _foldersToListen;
        private readonly ILogger _logger;
        private readonly IUnityContainer _unityContainer;

        public Application(
            ILocaleSelector localeSelector, 
            ISettings settings, 
            ILogger logger,
            IUnityContainer unityContainer)
        {
            _localeSelector = localeSelector;
            _foldersToListen = settings.Folders;
            _logger = logger;
            _unityContainer = unityContainer;
            FileBrokers = new List<IFileBroker>();
        }

        public List<IFileBroker> FileBrokers { get; set; }

        public void Run()
        {
            try
            {
                _localeSelector.SelectLocale();
                foreach (var folderToListen in _foldersToListen)
                {
                    IFileBroker broker = _unityContainer.Resolve<IFileBroker>();
                    broker.AttachToFolder(folderToListen);
                    FileBrokers.Add(broker);
                }
            }
            catch (ArgumentException exception)
            {
                _logger.Log(LogLevel.None, exception.Message);
            }
            catch (Exception exception)
            {
                _logger.Log(LogLevel.None, exception.Message);
            }
        }

        #region IDisposable
        ~Application()
        {
            DisposeInternal();
        }

        private void DisposeInternal()
        {
            foreach (var fileBroker in FileBrokers)
            {
                fileBroker.Dispose();
            }
        }

        public void Dispose()
        {
            DisposeInternal();
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}