namespace DirectoryListLibrary
{
    public class AppConfig : IConfig
    {
        public string QueueName { get; set; }
        public int IdleTimeout { get; set; }

    }
}

