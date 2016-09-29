using System;
using System.Messaging;
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
            _messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(IFileDetails) });

        }


        public void Push(IFileDetails file)
        {
            throw new NotImplementedException();
        }

        public IFileDetails Pull()
        {
            throw new NotImplementedException();
        }
    }
}
