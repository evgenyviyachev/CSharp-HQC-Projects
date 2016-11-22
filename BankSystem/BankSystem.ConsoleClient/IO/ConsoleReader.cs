namespace BankSystem.ConsoleClient.IO
{
    using System;
    using Interfaces;

    public class ConsoleReader : IReader
    {
        private const string exitCommand = "Exit";
        
        public void ReadCommands(IWriter writer, IInterpreter interpreter)
        {
            string input = Console.ReadLine();

            while (input.ToLower() != exitCommand.ToLower())
            {
                string[] data = input
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                string commandName = default(string);

                try
                {
                    commandName = data[0].ToLower();
                }
                catch (Exception)
                {
                    writer.WriteLine("Invalid command!");
                    this.ReadCommands(writer, interpreter);
                    return;
                }

                interpreter.InterpretCommand(writer, commandName, input, data);

                input = Console.ReadLine();
            }
        }
    }
}
