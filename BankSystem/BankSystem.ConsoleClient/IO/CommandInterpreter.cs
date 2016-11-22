namespace BankSystem.ConsoleClient.IO
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Attributes;
    using Interfaces;

    public class CommandInterpreter : IInterpreter
    {
        public void InterpretCommand(IWriter writer, string commandName, string input, string[] data)
        {
            try
            {
                ICommand command = this.ParseCommand(input, commandName, data);
                string output = command.Execute();
                writer.WriteLine(output);
            }
            catch (Exception ex)
            {
                writer.WriteLine(ex.Message);
            }
        }

        private ICommand ParseCommand(string input, string commandName, string[] data)
        {
            object[] parametersForConstruction = new object[]
            {
                input, data
            };

            Type typeOfCommand = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.GetCustomAttributes(typeof(AliasAttribute))
                .Where(atr => atr.Equals(commandName))
                .ToArray().Length > 0);

            if (typeOfCommand == null)
            {
                throw new InvalidOperationException("Invalid operation!");
            }

            //First way
            //var types = new Type[] { typeof(string), typeof(string[]) };
            //var constructor = typeOfCommand.GetConstructor(types);
            //var exe1 = (ICommand)constructor.Invoke(parametersForConstruction);

            //Second way
            var exe = (ICommand)Activator.CreateInstance(typeOfCommand, parametersForConstruction);

            return exe;
        }
    }
}
