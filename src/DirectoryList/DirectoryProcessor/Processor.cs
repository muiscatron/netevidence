using DirectoryListLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Messaging;

namespace DirectoryProcessor
{
    public class Processor
    {

        private static int fileCount;

        private IConfig _config;

        public Processor(IConfig config)
        {
            _config = config;
        }

        public async Task ProcessFolderAsync(string path, IProgress<IFileInfo> progress)
        {
            System.IO.DirectoryInfo root = new DirectoryInfo(path);

            await Task.Run(() =>
                {
                    //TODO: somehow get total number of files to allow for progress bar

                    WalkDirectoryTree(root, progress);
                }
            );
        }


        void WalkDirectoryTree(DirectoryInfo root, IProgress<IFileInfo> progress)
        {
            {

                System.IO.FileInfo[] files = null;
                System.IO.DirectoryInfo[] subDirs = null;

                try
                {
                    files = root.GetFiles("*.*");
                }
                catch (UnauthorizedAccessException ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                catch (DirectoryNotFoundException ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                if (files != null)
                {
                    foreach (System.IO.FileInfo fi in files)
                    {
                        fileCount++;
                        if (progress != null)
                        {
                            // Use progress report to pass the file info back to the UI thread
                            progress.Report(new DirectoryListLibrary.FileInfo() { Sequence = fileCount, FileName = fi.Name, FilePath = Path.GetDirectoryName(fi.DirectoryName) , Size = fi.Length, DateLastTouched = fi.LastAccessTime });
                        }
                    }

                    subDirs = root.GetDirectories();

                    foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                    {
                        WalkDirectoryTree(dirInfo, progress);
                    }
                }

            }




        }


        public async Task PopulateFromQueue(IProgress<IFileInfo> progress)
        {
            await Task.Run(() =>
            {
                MessageQueue messageQueue = new MessageQueue(_config.QueueName);
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(IFileInfo) });
                Message[] messages = messageQueue.GetAllMessages();

                foreach (Message message in messages)
                {
                    var info = (IFileInfo)message.Body;
                    progress.Report(new DirectoryListLibrary.FileInfo() { Sequence = fileCount, FileName = info.FileName, FilePath = info.FilePath, Size = info.Size, DateLastTouched = info.DateLastTouched});
                }
                // after all processing, delete all the messages
                messageQueue.Purge();
            }
);

        }



    }
}
