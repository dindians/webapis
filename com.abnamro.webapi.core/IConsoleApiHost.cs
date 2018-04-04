namespace com.abnamro.webapi.core
{
    public interface IConsoleApiHost
    {
        int BufferHeight { set; }
        string Title { set; }
        string[] Args { get; }
        void WriteLine(string value);
        string ReadLine();
    }
}
