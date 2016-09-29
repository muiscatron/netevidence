using System;

namespace DirectoryListLibrary
{
    [Serializable]
    public class FileDetails : IFileDetails
    {
        public DateTime DateLastTouched
        {
            get;

            set;
        }

        public string FileName
        {
            get;

            set;
        }

        public string FilePath
        {
            get;

            set;
        }
        public long FileSize
        {
                get;

                set;
        }

        public int Sequence
        {
            get;

            set;
        }


    }
}
