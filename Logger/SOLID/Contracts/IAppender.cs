namespace SOLID.Contracts
{
    using ReportLevels;

    public interface IAppender
    {
        void Append(string message, ReportLevel reportLevel);
        ILayout Layout { get; }
        ReportLevel ReportLevel { get; set; }
    }
}
