namespace DirectoryListLibrary
{
    public interface IConfig
    {
        string QueueName { get; set; }
        int PullBatchSize { get; set; }

        int PushBatchSize { get; set; }

        int IdleTimeout { get; set; }

    }
}
