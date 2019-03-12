using System;
using System.IO;
using FileSystemVisitor;
using Moq;
using NUnit.Framework;

namespace FileSystemVisitorTests
{
    public class FileVisitorTest
    {
        public const string EntityName = nameof(EntityName);
        public const string RootPath = nameof(RootPath);

        [Test]
        public void VisitFiles_NoFiltersRegistered_ReturnsAllEntities()
        {
            Mock<IFileSystem> fileSystem = SetupFileSystem(10);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object);
            var result = visitor.Visit(RootPath);
            Assert.AreEqual(10, result.Length);
        }

        [Test]
        public void VisitFiles_AbortVisitingInFileFoundedEvent_VisitingAborted()
        {
            Mock<IFileSystem> fileSystem = SetupFileSystem(10);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object);
            visitor.FileFounded += (sender, args) => throw new VisitingAbortedException();
            var result = visitor.Visit(RootPath);
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void VisitFiles_SubscribeToStartEvent_EventWasCalled()
        {
            bool eventWasCalled = false;
            Mock<IFileSystem> fileSystem = SetupFileSystem(10);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object);
            visitor.VisitingStarted += (sender, args) => eventWasCalled = true;
            visitor.Visit(RootPath);
            Assert.IsTrue(eventWasCalled);
        }

        [Test]
        public void VisitFiles_SubscribeToFinishEvent_EventWasCalled()
        {
            bool eventWasCalled = false;
            Mock<IFileSystem> fileSystem = SetupFileSystem(10);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object);
            visitor.VisitingFinished += (sender, args) => eventWasCalled = true;
            var result = visitor.Visit(RootPath);
            Assert.IsTrue(eventWasCalled);
        }

        [Test]
        public void VisitFiles_FiltersRegistered_ReturnsAllFilteredEntities()
        {
            Mock<IFileSystem> fileSystem = SetupFileSystem(10);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object, 
                info => info.FullName.Contains(EntityName), info => info.FullName.Contains(EntityName));
            var result = visitor.Visit(RootPath);
            Assert.AreEqual(10, result.Length);
        }

        [Test]
        public void VisitFiles_FiltersRegistered_ReturnsZeroEntities()
        {
            Mock<IFileSystem> fileSystem = SetupFileSystem(10);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object,
                info => info.Exists, info => info.Exists);
            var result = visitor.Visit(RootPath);
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void VisitFiles_SubscribeToFileFoundedEvent_SkipHalfOfElements()
        {
            var boolValue = false;
            Mock<IFileSystem> fileSystem = SetupFileSystem(10);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object);
            visitor.FileFounded += (sender, args) =>
            {
                boolValue = !boolValue;
                args.IsSkipVisitedEntity = boolValue;
            };
            var result = visitor.Visit(RootPath);
            Assert.AreEqual(5, result.Length);
        }

        [Test]
        public void VisitFiles_SetupFilter_SubscribeToFileFoundedEvent_SkipsHalfOfElementsAndAppliesFilter()
        {
            var boolValue = false;
            var boolValue2 = false;
            Mock<IFileSystem> fileSystem = SetupFileSystem(20);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object,
                info =>
                {
                    boolValue2 = !boolValue2;
                    return boolValue2;
                }, info => info.Exists);
            visitor.FileFounded += (sender, args) =>
            {
                boolValue = !boolValue;
                args.IsSkipVisitedEntity = boolValue;
            };
            var result = visitor.Visit(RootPath);
            Assert.AreEqual(5, result.Length);
        }

        [Test]
        public void VisitFiles_SetupFilter_SubscribeToFilteredFileFoundedEvent_SkipsHalfOfElementsAndAppliesFilter()
        {
            var boolValue = false;
            var boolValue2 = false;
            Mock<IFileSystem> fileSystem = SetupFileSystem(20);
            IFileSystemVisitor visitor = new FileSystemVisitor.FileSystemVisitor(fileSystem.Object,
                info =>
                {
                    boolValue2 = !boolValue2;
                    return boolValue2;
                }, info => info.Exists);
            visitor.FilteredFileFounded += (sender, args) =>
            {
                boolValue = !boolValue;
                args.IsSkipVisitedEntity = boolValue;
            };
            var result = visitor.Visit(RootPath);
            Assert.AreEqual(5, result.Length);
        }

        private Mock<IFileSystem> SetupFileSystem(int numberOfFiles)
        {
            var fileSystemMock = new Mock<IFileSystem>();
            var filesArray = new FileInfo[numberOfFiles];
            for (int i = 0; i < numberOfFiles; i++)
            {
                filesArray[i] = new FileInfo(EntityName);
            }

            fileSystemMock.Setup(system => system.GetFiles(It.IsAny<DirectoryInfo>())).Returns(filesArray);
            fileSystemMock.Setup(system => system.GetDirectories(It.IsAny<DirectoryInfo>())).Returns(Array.Empty<DirectoryInfo>());
            fileSystemMock.Setup(system => system.DirectoryExists(RootPath)).Returns(true);
            fileSystemMock.Setup(system => system.ProvideDirectoryInfo(RootPath)).Returns(new DirectoryInfo(EntityName));
            return fileSystemMock;
        }
    }
}