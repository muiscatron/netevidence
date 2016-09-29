using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryListLibrary;

namespace FileQueueProcessor.Memory
{
    public class ListProcessor : IFileQueueProcessor
    {
        private  Queue<IFileInfo> _fileList;


        public ListProcessor()
        {
            _fileList = new Queue<IFileInfo>();
            
        }

        public void Push(IFileInfo file)
        {
            _fileList.Enqueue(file);
        }

        public IFileInfo Pull()
        {
            if (_fileList.Count != 0)
            {
                var item = _fileList.Dequeue();
                return item;
            }
            return null;

        }
    }
}
