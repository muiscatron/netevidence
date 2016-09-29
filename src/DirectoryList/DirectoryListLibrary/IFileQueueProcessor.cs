namespace DirectoryListLibrary
{
    public interface IFileQueueProcessor
    {
        void Push(IFileDetails file);
        IFileDetails Pull();
    }
}
