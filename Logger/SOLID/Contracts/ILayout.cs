namespace SOLID.Contracts
{
    using ReportLevels;

    public interface ILayout
    {
        string ConstructMessage(string message, ReportLevel reportLevel);
    }
}
