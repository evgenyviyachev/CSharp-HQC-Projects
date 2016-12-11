namespace SOLID.Contracts
{
    public interface ILogger
    {
        IAppender[] Appenders { get; set; }
        void Info(string message);
        void Error(string message);
        void Warn(string message);
        void Critical(string message);
        void Fatal(string message);
    }
}
