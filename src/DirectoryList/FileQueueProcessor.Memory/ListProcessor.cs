using System.Collections.Generic;
using DirectoryListLibrary;

namespace FileQueueProcessor.Memory
{
    public class ListProcessor : IFileQueueProcessor
    {
        private readonly Queue<IFileDetails> _fileList;


        public ListProcessor()
        {
            _fileList = new Queue<IFileDetails>();
            
        }

        public void Push(IFileDetails file)
        {
            _fileList.Enqueue(file);
        }

        public IFileDetails Pull()
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
