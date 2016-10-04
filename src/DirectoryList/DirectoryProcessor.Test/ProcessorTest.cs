using System;
using System.IO;
using DirectoryListLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;
using System.Linq;

namespace DirectoryProcessor.Test
{
    [TestClass]
    public class ProcessorTest
    {
        [TestMethod]
        public void WalkDirectoryTree_Returns_All_Files_Including_SubDirectories()
        {

            var container = new AutoMocker();


            FileInfo[] topLevelFiles = new FileInfo[] {new FileInfo(@"h:\file1.txt"), new FileInfo(@"h:\file2.txt"), new FileInfo(@"h:\file3.txt") };

            DirectoryInfo[] topLevelDirectories = new DirectoryInfo[] {new DirectoryInfo(@"h:\dir1")};

            FileInfo[] dir1Files = new FileInfo[] { new FileInfo(@"h:\d1file1.txt"), new FileInfo(@"h:\d1file2.txt") };

            var wrapper = container.GetMock<IDirectoryInfoWrapper>();

            // Need to send a mock for this to ensure that the progress reporter is called
            var progress = container.GetMock<IProgress<int>>();

            var queueProcessor = container.GetMock<IFileQueueProcessor>();

            // Set up the files to be found - top level then subdir level
            wrapper.Setup(r => r.GetFiles(It.IsAny<DirectoryInfo>())).ReturnsInOrder(topLevelFiles, dir1Files);

            wrapper.Setup(r => r.GetDirectories(It.IsAny<DirectoryInfo>())).Returns(topLevelDirectories);

            var processor = container.CreateInstance<Processor>();

            var result = processor.ProcessDirectoryAsync(@"h:\", progress.Object);

            //NOTE - currently test doesn't work because concrete implementation of FileInfo is being used
            // need to provide a wrapper or abstraction for FileInfo

            queueProcessor.Verify(x => x.Push(It.Is<IFileDetails>(y => y.FileName == "file1.txt")));
            queueProcessor.Verify(x => x.Push(It.Is<IFileDetails>(y => y.FileName == "file2.txt")));
            queueProcessor.Verify(x => x.Push(It.Is<IFileDetails>(y => y.FileName == "file3.txt")));
            queueProcessor.Verify(x => x.Push(It.Is<IFileDetails>(y => y.FileName == "d1file1.txt")));
            queueProcessor.Verify(x => x.Push(It.Is<IFileDetails>(y => y.FileName == "d1file2.txt")));


        }
    }
}
