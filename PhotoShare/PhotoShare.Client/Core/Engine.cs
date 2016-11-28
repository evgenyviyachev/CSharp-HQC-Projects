namespace PhotoShare.Client.Core
{
    using System;
    using Interfaces;

    public class Engine : IRunnable
    {
        private ICommandDispatcher commandDispatcher;
        private IReader reader;
        private IWriter writer;

        public Engine(ICommandDispatcher commandDispatcher, IReader reader, IWriter writer)
        {
            this.commandDispatcher = commandDispatcher;
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            this.writer.WriteLine("Program started");
            string input = this.reader.ReadLine();

            while (true)
            {
                try
                {
                    string[] data = input.Split(
                        new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    string commandName = data[0];

                    string result = this.commandDispatcher
                        .DispatchCommand(commandName, data)
                        .Execute();

                    this.writer.WriteLine(result);
                }
                catch (Exception e)
                {
                    this.writer.WriteLine(e.Message);
                }
                finally
                {
                    input = this.reader.ReadLine();
                }
            }
        }
    }
}
