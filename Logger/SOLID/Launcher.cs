namespace SOLID
{
    using Appenders;
    using Contracts;
    using Layouts;
    using ReportLevels;
    using System.Globalization;
    using System.Threading;

    public class Launcher
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CurrentUICulture;

            //------------------------------------------------
            //Test 1 - Simple Layout on Console
            ILayout simpleLayout = new SimpleLayout();
            IAppender consoleAppender =
                 new ConsoleAppender(simpleLayout);
            ILogger logger = Logger.GetInstance(consoleAppender);

            logger.Error("Error parsing JSON.");
            logger.Info(string.Format("User {0} successfully registered.", "Pesho"));

            //------------------------------------------------
            //Test 2 - Simple Layout on Console & File
            simpleLayout = new SimpleLayout();

            consoleAppender = new ConsoleAppender(simpleLayout);
            FileAppender fileAppender = new FileAppender(simpleLayout);
            fileAppender.File = "log.txt";

            logger = Logger.GetInstance(consoleAppender, fileAppender);
            logger.Error("Error parsing JSON.");
            logger.Info(string.Format("User {0} successfully registered.", "Pesho"));
            logger.Warn("Warning - missing files.");

            //------------------------------------------------
            //Test 3 - XML Layout on Console
            ILayout xmlLayout = new XmlLayout();
            consoleAppender = new ConsoleAppender(xmlLayout);
            logger = Logger.GetInstance(consoleAppender);

            logger.Fatal("mscorlib.dll does not respond");
            logger.Critical("No connection string found in App.config");

            //------------------------------------------------
            //Test 4 - Levels of reporting - only reports with HIGHER level
            simpleLayout = new SimpleLayout();
            consoleAppender = new ConsoleAppender(simpleLayout);
            consoleAppender.ReportLevel = ReportLevel.Error;

            logger = Logger.GetInstance(consoleAppender);

            logger.Info("Everything seems fine");
            logger.Warn("Warning: ping is too high - disconnect imminent");
            logger.Error("Error parsing request");
            logger.Critical("No connection string found in App.config");
            logger.Fatal("mscorlib.dll does not respond");
        }
    }
}
