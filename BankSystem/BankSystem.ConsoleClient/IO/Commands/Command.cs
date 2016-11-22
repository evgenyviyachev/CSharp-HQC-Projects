namespace BankSystem.ConsoleClient.IO.Commands
{
    using Interfaces;
    using System;

    public abstract class Command : ICommand
    {
        private string input;
        private string[] data;

        public Command(string input, string[] data)
        {
            this.Input = input;
            this.Data = data;
        }

        public string Input
        {
            get
            {
                return this.input;
            }
            private set
            {
                if (value == null || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Command cannot be null or empty!");
                }

                this.input = value;
            }
        }

        public string[] Data
        {
            get
            {
                return this.data;
            }
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new InvalidOperationException("This is not a valid command!");
                }

                this.data = value;
            }
        }
        
        public abstract string Execute();
    }
}
