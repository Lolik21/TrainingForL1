using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using TaskForModule5.Localization;
using TaskForModule5.Settings;

namespace TaskForModule5.FileBroker
{
    public class FileBroker : IFileBroker
    {
        private string _folderPath;
        private FileSystemWatcher _fileSystemWatcher;
        private readonly ICustomStringLocalizer _customStringLocalizer;
        private readonly ILogger _logger;
        private readonly ISettings _settings;

        public FileBroker(
            ILogger logger,
            ISettings settings,
            ICustomStringLocalizer customStringLocalizer)
        {
            _logger = logger;
            _settings = settings;
            _customStringLocalizer = customStringLocalizer;
        }

        public void AttachToFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                throw new ArgumentException(string.Format(_customStringLocalizer["FolderNotFounded"], folderPath));
            }

            _folderPath = folderPath;
            _fileSystemWatcher = new FileSystemWatcher(folderPath);
            _logger.Log(LogLevel.None, string.Format(_customStringLocalizer["BrokerAttached"], folderPath));
            _fileSystemWatcher.Created += OnFileCreated;
            _fileSystemWatcher.EnableRaisingEvents = true;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            _logger.Log(LogLevel.None, string.Format(_customStringLocalizer["EntityArrived"], e.FullPath));
            if (!IsDirectory(e.FullPath))
            {
                ApplyRuleSetForFile(e.FullPath, e.Name);
            }
        }

        private void ApplyRuleSetForFile(string filePath, string fileName)
        {
            foreach (var rule in _settings.Rules)
            {
                if (Regex.IsMatch(fileName, rule.Key))
                {
                    _logger.Log(LogLevel.None, string.Format(_customStringLocalizer["RuleApply"], rule.Key, fileName));
                    MoveFileToFolder(filePath, fileName, rule.Value);
                    return;
                }
            }

            _logger.Log(LogLevel.None, string.Format(_customStringLocalizer["NoRuleFounded"], fileName));
            MoveFileToFolder(filePath, fileName, "DefaultFolder");
        }

        private void MoveFileToFolder(string filePath, string fileName, string folderSelector)
        {
            var ruleDirectory = CreateSubFolderIfRequired(_folderPath, folderSelector);
            var destinationPath = Path.Combine(ruleDirectory, fileName);
            var newPath = AddDateToFileName(destinationPath);
            newPath = AddNumberToFileName(newPath);
            File.Move(filePath, newPath);
            _logger.Log(LogLevel.None, string.Format(_customStringLocalizer["FileMoved"], filePath, newPath));
        }

        private string AddDateToFileName(string destinationPath)
        {
            if (_settings.AddDate)
            {
                var fileInfo = new FileInfo(destinationPath);
                var dir = fileInfo.DirectoryName;
                var name = fileInfo.Name.Split(new[] { '.' })[0];
                var extension = fileInfo.Name.Split(new[] { '.' })[1];
                Regex regex = new Regex("[^a-zA-Z0-9 -]");
                var dateTime = regex.Replace(DateTime.Now.ToString(CultureInfo.CurrentUICulture),
                    string.Empty);
                destinationPath = $"{dir}\\{name}({dateTime}).{extension}";
            }

            return destinationPath;
        }

        private string AddNumberToFileName(string destinationPath)
        {
            if (_settings.AddNumber)
            {
                if (File.Exists(destinationPath))
                {
                    var number = 0;
                    var fileInfo = new FileInfo(destinationPath);
                    var dir = fileInfo.DirectoryName;
                    var name = fileInfo.Name.Split(new[] { '.' })[0];
                    var extension = fileInfo.Name.Split(new[] { '.' })[1];
                    while (File.Exists(destinationPath))
                    {
                        destinationPath = $"{dir}\\{name}{number}.{extension}";
                        number++;
                    }
                }
            }

            return destinationPath;
        }

        private string CreateSubFolderIfRequired(string path, string folderSelector)
        {
            if (_customStringLocalizer[folderSelector] != null)
            {
                folderSelector = _customStringLocalizer[folderSelector];
            }

            var folderPath = Path.Combine(path, folderSelector);
            if (!Directory.Exists(folderPath))
            {
                return Directory.CreateDirectory(folderPath).FullName;
            }

            return folderPath;
        }

        private static bool IsDirectory(string path)
        {
            FileAttributes file = File.GetAttributes(path);
            return (file & FileAttributes.Directory) != 0;
        }

        private void DisposeInternal()
        {
            _fileSystemWatcher.Dispose();
        }

        public void Dispose()
        {
            DisposeInternal();
            GC.SuppressFinalize(this);
        }

        ~FileBroker()
        {
            DisposeInternal();
        }
    }
}