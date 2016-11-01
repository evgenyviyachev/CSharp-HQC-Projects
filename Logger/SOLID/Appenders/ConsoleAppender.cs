namespace SOLID.Appenders
{
    using System;
    using ReportLevels;
    using Contracts;

    public class ConsoleAppender : Appender
    {
        public ConsoleAppender(ILayout layout)
            : base(layout)
        {
        }

        public override void Append(string message, ReportLevel reportLevel)
        {
            if ((int)this.ReportLevel <= (int)reportLevel)
            {
                Console.WriteLine(this.Layout.ConstructMessage(message, reportLevel));
            }
        }
    }
}
