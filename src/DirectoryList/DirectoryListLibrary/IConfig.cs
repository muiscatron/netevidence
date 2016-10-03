namespace DirectoryListLibrary
{
    public interface IConfig
    {
        string QueueName { get; set; }
        int IdleTimeout { get; set; }
}
}
