using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryListLibrary
{
    public class DirectoryInfoWrapper : IDirectoryInfoWrapper
    {
        public DirectoryInfo[] GetDirectories(DirectoryInfo directory)
        {
            return directory.GetDirectories();
        }

        public DirectoryInfo GetDirectoryInfo(string path)
        {
            return new DirectoryInfo(path);
        }

        public FileInfo[] GetFiles(DirectoryInfo directory)
        {
            return directory.GetFiles("*.*");

        }
    }
}
