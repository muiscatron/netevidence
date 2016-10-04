using System;
using System.Diagnostics;
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

            try
            {
                if (MessageQueue.Exists(_config.QueueName))
                {
                    _messageQueue = new MessageQueue(_config.QueueName);
                }
                else
                {
                    _messageQueue = MessageQueue.Create(_config.QueueName);
                }
                _messageQueue.Formatter = new XmlMessageFormatter(new[] { typeof(FileDetails) });
                _messageQueue.Purge();

            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine(ex.Message);
                throw new Exception("Message Queuing is not installed");
            }

        }


        public void Push(IFileDetails file)
        {
            try
            {
                var msg = new Message();
                msg.Label = file.FileName;
                msg.Body = file;
                _messageQueue.Send(msg);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error when sending to queue : {0}", ex.Message);
            }
        }

        public IFileDetails Pull()
        {
            try
            {
                // Something makes this whole thread abort when the Receive() method returns no data - the exception handler
                // does not seem to work
                FileDetails item = (FileDetails)_messageQueue.Receive().Body;
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"Error when receiving from queue : {0}", ex.Message);
                return null;

            }
        }
    }
}
