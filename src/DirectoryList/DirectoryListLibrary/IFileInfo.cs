using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryListLibrary
{
    public interface IFileInfo
    {
        int Sequence { get; set; }
        string FileName { get; set; }
        string FilePath { get; set; }
        long Size { get; set; }
        DateTime DateLastTouched { get; set; }
    }
}
