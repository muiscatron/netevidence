using DirectoryListLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirectoryProcessor
{
    public class Processor
    {

        public Processor(IConfig config)
        {

        }

        //https://blogs.msdn.microsoft.com/dotnet/2012/06/06/async-in-4-5-enabling-progress-and-cancellation-in-async-apis/

        Task<IFileInfo> ProcessFolderAsync(string path, IProgress<IFileInfo> progress)
        {
            return Task.Run<IFileInfo>(() =>
            {
                System.IO.DirectoryInfo root = new DirectoryInfo(path);

                System.IO.FileInfo[] files = null;
                System.IO.DirectoryInfo[] subDirs = null;

                // First, process all the files directly under this folder
                try
                {
                    files = root.GetFiles("*.*");
                }
                // This is thrown if even one of the files requires permissions greater
                // than the application provides.
                catch (UnauthorizedAccessException e)
                {
                    // This code just writes out the message and continues to recurse.
                    // You may decide to do something different here. For example, you
                    // can try to elevate your privileges and access the file again.
                    log.Add(e.Message);
                }

                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }

                if (files != null)
                {
                    foreach (System.IO.FileInfo fi in files)
                    {
                        // In this example, we only access the existing FileInfo object. If we
                        // want to open, delete or modify the file, then
                        // a try-catch block is required here to handle the case
                        // where the file has been deleted since the call to TraverseTree().
                        Console.WriteLine(fi.FullName);
                        if (progress != null)
                        {
                            progress.Report(new DirectoryListLibrary.FileInfo() { FileName = fi.Name, FilePath = fi.DirectoryName, Size = fi.Length, DateLastTouched = fi.LastAccessTime });
                        }
                    }

                    // Now find all the subdirectories under this directory.
                    subDirs = root.GetDirectories();

                    foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                    {
                        // Resursive call for each subdirectory.
                        WalkDirectoryTree(dirInfo);
                    }
                }

            }
                
                

            );
        }


        void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {

        }


    }
}
