namespace SOLID.Layouts
{
    using System;
    using Contracts;
    using ReportLevels;
    using System.Text;

    public class XmlLayout : ILayout
    {
        public string ConstructMessage(string message, ReportLevel reportLevel)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<log>");
            sb.AppendLine($"    <date>{DateTime.Now}</date>");
            sb.AppendLine($"    <level>{reportLevel}</level>");
            sb.AppendLine($"    <message>{message}</message>");
            sb.Append("</log>");

            return sb.ToString();
        }
    }
}
