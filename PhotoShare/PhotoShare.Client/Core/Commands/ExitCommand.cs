namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;
    using System.Data.Entity;

    public class ExitCommand : Command
    {
        public ExitCommand(string[] data)
            : base(data)
        {
        }

        public override string Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            Environment.Exit(0);
            return "Bye-bye";
        }
    }
}
