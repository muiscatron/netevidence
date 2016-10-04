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
        private readonly IDirectoryInfoWrapper _directoryInfoWrapper;

        public Processor(IFileQueueProcessor fileQueueProcessor, IConfig config, IDirectoryInfoWrapper directoryInfoWrapper)
        {
            _fileQueueProcessor = fileQueueProcessor;
            _config = config;
            _directoryInfoWrapper = directoryInfoWrapper;
        }

        public async Task ProcessDirectoryAsync(string path, IProgress<int> progress)
        {
            var root = _directoryInfoWrapper.GetDirectoryInfo(path);

            await Task.Run(() =>
                {

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
                    files = _directoryInfoWrapper.GetFiles(root);
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
                            var info = new FileDetails { Sequence = _fileCount, FileName = fi.Name, FilePath = fi.DirectoryName, FileSize = fi.Length, DateLastTouched = fi.LastAccessTime };
                            progress.Report(_fileCount);
                            _fileQueueProcessor.Push(info);

                        }
                    }

                    var subDirs = _directoryInfoWrapper.GetDirectories(root);

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
