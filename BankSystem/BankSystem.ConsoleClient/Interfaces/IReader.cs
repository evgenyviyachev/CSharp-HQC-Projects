namespace BankSystem.ConsoleClient.Interfaces
{
    public interface IReader
    {
        void ReadCommands(IWriter writer, IInterpreter interpreter);
    }
}
