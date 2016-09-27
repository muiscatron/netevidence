using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryListLibrary
{
    public interface IFileQueue
    {
        void Push(IFileInfo file);
        IFileInfo Pop();
    }
}
