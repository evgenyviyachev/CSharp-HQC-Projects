namespace BankSystem.ConsoleClient.Interfaces
{
    public interface IInterpreter
    {
        void InterpretCommand(IWriter writer, string command, string input, string[] data);
    }
}
