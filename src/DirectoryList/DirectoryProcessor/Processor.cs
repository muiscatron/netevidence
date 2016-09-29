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

        private IFileQueueProcessor _fileQueueProcessor;

        public Processor(IConfig config, IFileQueueProcessor fileQueueProcessor)
        {
            _config = config;
            _fileQueueProcessor = fileQueueProcessor;
        }

        public async Task ProcessFolderAsync(string path, IProgress<int> progress)
        {
            System.IO.DirectoryInfo root = new DirectoryInfo(path);

            await Task.Run(() =>
                {
                    //TODO: somehow get total number of files to allow for progress bar

                    WalkDirectoryTree(root, progress);
                }
            );
        }


        void WalkDirectoryTree(DirectoryInfo root, IProgress<int> progress)
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
                            var info = new DirectoryListLibrary.FileInfo { Sequence = fileCount, FileName = fi.Name, FilePath = Path.GetDirectoryName(fi.DirectoryName) , Size = fi.Length, DateLastTouched = fi.LastAccessTime };
                            progress.Report(fileCount);
                            _fileQueueProcessor.Push(info);

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
                bool c = true;

                while (c)
                {
                    var info = _fileQueueProcessor.Pull();
                    if (info != null)
                    {
                        progress.Report(new DirectoryListLibrary.FileInfo() { Sequence = fileCount, FileName = info.FileName, FilePath = info.FilePath, Size = info.Size, DateLastTouched = info.DateLastTouched });
                    }
                }

            }
            );

        }



    }
}
