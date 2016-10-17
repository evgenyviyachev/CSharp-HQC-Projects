using RecyclingStationNS.Contracts;
using System;

namespace RecyclingStationNS.InputManager
{
    public class Engine : IEngine
    {
        private ICommandInterpreter interpreter;

        public Engine(ICommandInterpreter interpreter)
        {
            this.interpreter = interpreter;
        }

        public Engine()
            : this(new CommandInterpreter())
        {
        }

        public void Run()
        {
            string input = Console.ReadLine();

            while (input != "TimeToRecycle")
            {
                string[] data = input.Trim().Split();

                string result = this.interpreter.InterpretCommand(data);

                Console.WriteLine(result);

                input = Console.ReadLine();
            }
        }
    }
}
