namespace SOLID.Layouts
{
    using System;
    using Contracts;
    using ReportLevels;

    public class SimpleLayout : ILayout
    {
        public string ConstructMessage(string message, ReportLevel reportLevel)
        {
            return $"{DateTime.Now} - {reportLevel} - {message}";
        }
    }
}
