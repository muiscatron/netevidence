using DirectoryListLibrary;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace DirectoryProcessor
{
    public class Processor
    {

        private static int _fileCount;

        private IConfig _config;

        private readonly IFileQueueProcessor _fileQueueProcessor;

        public Processor(IFileQueueProcessor fileQueueProcessor, IConfig config)
        {
            _fileQueueProcessor = fileQueueProcessor;
            _config = config;
        }

        public async Task ProcessDirectoryAsync(string path, IProgress<int> progress)
        {
            var root = new DirectoryInfo(path);

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

                FileInfo[] files = null;

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
                    foreach (FileInfo fi in files)
                    {
                        _fileCount++;
                        if (progress != null)
                        {
                            var info = new FileDetails { Sequence = _fileCount, FileName = fi.Name, FilePath = Path.GetDirectoryName(fi.DirectoryName) , FileSize = fi.Length, DateLastTouched = fi.LastAccessTime };
                            progress.Report(_fileCount);
                            _fileQueueProcessor.Push(info);

                        }
                    }

                    var subDirs = root.GetDirectories();

                    foreach (var dirInfo in subDirs)
                    {
                        WalkDirectoryTree(dirInfo, progress);
                    }
                }

            }




        }


        public async Task PopulateFromQueueAsync(IProgress<IFileDetails> progress)
        {
            // Run forever, pulling from the queue
            await Task.Run(() =>
            {

                DateTime lastFileReceived = DateTime.Now;


                while (true)
                {
                    var info = _fileQueueProcessor.Pull();

                    if (info != null)
                    {
                        lastFileReceived = DateTime.Now;
                        var fileDetails = new FileDetails() { Sequence = info.Sequence, FileName = info.FileName, FilePath = info.FilePath, FileSize = info.FileSize, DateLastTouched = info.DateLastTouched };

                        progress.Report(fileDetails);

                    }

                    else
                    {
                        
                        if (DateTime.Now > lastFileReceived.AddSeconds(_config.IdleTimeout))
                        {
                            break;
                        }
                        

                    }
                }

                // ReSharper disable once FunctionNeverReturns
            }
            );

        }



    }
}
