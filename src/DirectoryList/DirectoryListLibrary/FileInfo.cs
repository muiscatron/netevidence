using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryListLibrary
{
    public class FileInfo : IFileInfo
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
        public long Size
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
