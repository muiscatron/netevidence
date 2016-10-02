namespace DirectoryListLibrary
{
    public class AppConfig : IConfig
    {
        public string QueueName
        { get; set; }

        public int PullBatchSize { get; set; }

        public int PushBatchSize { get; set; }

        public int IdleTimeout { get; set; }

    }
}

