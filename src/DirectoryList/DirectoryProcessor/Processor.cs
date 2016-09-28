using DirectoryListLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace DirectoryProcessor
{
    public class Processor
    {

        private static int fileCount;

        public Processor(IConfig config)
        {

        }

        public async Task ProcessFolderAsync(string path, IProgress<IFileInfo> progress)
        {
            System.IO.DirectoryInfo root = new DirectoryInfo(path);

            await Task.Run(() => WalkDirectoryTree(root, progress));
        }


        void WalkDirectoryTree(System.IO.DirectoryInfo root, IProgress<IFileInfo> progress)
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

                catch (System.IO.DirectoryNotFoundException ex)
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


    }
}
