namespace LambdaCore_Skeleton.CommandInterpreter
{
    using System;
    using LambdaCore_Skeleton.Contracts;

    public class Engine : IEngine
    {
        private ICommandInterpreter interpreter = new CommandInterpreter();

        public void Run()
        {
            string input = Console.ReadLine();

            while (input != "System Shutdown!")
            {
                string[] data = input.Split('@');
                string commandName = data[0].Trim(':');
                string[] parameters = new string[data.Length - 1];

                for (int i = 1; i < data.Length; i++)
                {
                    parameters[i - 1] = data[i];
                }

                Console.WriteLine(interpreter.InterpretCommand(commandName, parameters));

                input = Console.ReadLine();
            }
        }
    }
}
