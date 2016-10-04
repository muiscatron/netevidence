using System;
using System.IO;
using DirectoryListLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.AutoMock;

namespace DirectoryProcessor.Test
{
    [TestClass]
    public class ProcessorTest
    {
        [TestMethod]
        public void WalkDirectoryTree_Returns_All_Files_Including_SubDirectories()
        {

            var container = new AutoMocker();


            FileInfo[] topLevelFiles = new FileInfo[] {new FileInfo(@"h:\file1.txt")};


            var wrapper = container.GetMock<IDirectoryInfoWrapper>();

            wrapper.Setup(r => r.GetFiles(It.IsAny<DirectoryInfo>())).Returns(topLevelFiles);

            var store = container.CreateInstance<Processor>();

            var result = store.ProcessDirectoryAsync(@"h:\", null);



        }
    }
}
