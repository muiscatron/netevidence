using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using DirectoryListLibrary;

namespace FileQueueProcessor.MSMQ
{
    public class MsmqProcessor : IFileQueueProcessor
    {

        private MessageQueue _messageQueue;
        private IConfig _config;

        public MsmqProcessor(IConfig config)
        {
            _config = config;
            _messageQueue = new MessageQueue(_config.QueueName);
            _messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(IFileInfo) });

        }


        public void Push(IFileInfo file)
        {
            throw new NotImplementedException();
        }

        public IFileInfo Pull()
        {
            throw new NotImplementedException();
        }
    }
}
