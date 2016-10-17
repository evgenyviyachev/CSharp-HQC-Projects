namespace RecyclingStationNS.Contracts
{
    public interface ICommandInterpreter
    {
        string InterpretCommand(string[] data);
    }
}
