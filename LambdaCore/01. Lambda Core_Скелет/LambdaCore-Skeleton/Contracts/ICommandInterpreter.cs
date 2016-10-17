namespace LambdaCore_Skeleton.Contracts
{
    public interface ICommandInterpreter
    {
        string InterpretCommand(string name, string[] parameters);
    }
}
