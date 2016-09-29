using System;

namespace DirectoryListLibrary
{
    public interface IFileDetails
    {
        int Sequence { get; set; }
        string FileName { get; set; }
        string FilePath { get; set; }
        long FileSize { get; set; }
        DateTime DateLastTouched { get; set; }
    }
}
