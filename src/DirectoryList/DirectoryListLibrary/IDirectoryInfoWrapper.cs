using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryListLibrary
{
    public interface IDirectoryInfoWrapper
    {
        DirectoryInfo GetDirectoryInfo(string path);

        FileInfo[] GetFiles(DirectoryInfo directory);

        DirectoryInfo[] GetDirectories(DirectoryInfo directory);


    }
}
