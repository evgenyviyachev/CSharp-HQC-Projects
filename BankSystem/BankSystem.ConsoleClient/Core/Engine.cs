namespace BankSystem.ConsoleClient.Core
{
    using Interfaces;
    using IO;

    public class Engine : IEngine
    {
        private IReader reader;
        private IInterpreter interpreter;
        private IWriter writer;

        public Engine(IReader reader, IInterpreter interpreter, IWriter writer)
        {
            this.reader = reader;
            this.interpreter = interpreter;
            this.writer = writer;
        }

        public Engine()
            : this(new ConsoleReader(), new CommandInterpreter(), new ConsoleWriter())
        {
        }

        public void Run()
        {
            this.reader.ReadCommands(writer, interpreter);
        }
    }
}
