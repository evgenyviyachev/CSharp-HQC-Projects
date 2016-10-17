using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Executor.Exceptions;
using Executor.Network;
using Executor.Repository;
using Executor.Judge;
using Executor.Contracts;

namespace Executor.IO.Commands
{
    public abstract class Command : IExecutable
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
            get { return this.input; }

            private set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException(value);
                }

                this.input = value;
            }
        }

        public string[] Data
        {
            get { return this.data; }

            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new InvalidCommandException(this.Input);
                }

                this.data = value;
            }
        }

        public abstract void Execute();
    }
}
