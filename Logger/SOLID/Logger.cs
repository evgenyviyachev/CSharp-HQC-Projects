namespace SOLID
{
    using Contracts;
    using ReportLevels;

    public class Logger : ILogger
    {
        private IAppender[] appenders;

        public Logger(params IAppender[] appenders)
        {
            this.appenders = appenders;
        }

        public void Critical(string message)
        {
            foreach (var appender in this.appenders)
            {
                appender.Append(message, ReportLevel.Critical);
            }            
        }

        public void Error(string message)
        {
            foreach (var appender in this.appenders)
            {
                appender.Append(message, ReportLevel.Error);
            }
        }

        public void Fatal(string message)
        {
            foreach (var appender in this.appenders)
            {
                appender.Append(message, ReportLevel.Fatal);
            }
        }

        public void Info(string message)
        {
            foreach (var appender in this.appenders)
            {
                appender.Append(message, ReportLevel.Info);
            }
        }

        public void Warn(string message)
        {
            foreach (var appender in this.appenders)
            {
                appender.Append(message, ReportLevel.Warning);
            }
        }
    }
}
