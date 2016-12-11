namespace SOLID
{
    using Contracts;
    using ReportLevels;

    public class Logger : ILogger
    {
        private static ILogger _instance;
        
        private Logger()
        {
        }

        public IAppender[] Appenders { get; set; }

        public static ILogger GetInstance(params IAppender[] appenders)
        {
            if (_instance == null)
            {
                _instance = new Logger();
            }

            _instance.Appenders = appenders;

            return _instance;
        }

        public void Critical(string message)
        {
            foreach (var appender in this.Appenders)
            {
                appender.Append(message, ReportLevel.Critical);
            }
        }

        public void Error(string message)
        {
            foreach (var appender in this.Appenders)
            {
                appender.Append(message, ReportLevel.Error);
            }
        }

        public void Fatal(string message)
        {
            foreach (var appender in this.Appenders)
            {
                appender.Append(message, ReportLevel.Fatal);
            }
        }

        public void Info(string message)
        {
            foreach (var appender in this.Appenders)
            {
                appender.Append(message, ReportLevel.Info);
            }
        }

        public void Warn(string message)
        {
            foreach (var appender in this.Appenders)
            {
                appender.Append(message, ReportLevel.Warning);
            }
        }
    }
}
