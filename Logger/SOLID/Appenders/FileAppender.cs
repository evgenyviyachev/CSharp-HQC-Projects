namespace SOLID.Appenders
{
    using System;
    using Contracts;
    using ReportLevels;

    public class FileAppender : Appender
    {
        private string file;
        public FileAppender(ILayout layout)
            : base(layout)
        {
        }

        public string File
        {
            get
            {
                return this.file;
            }

            set
            {
                this.file = value;
            }
        }

        public override void Append(string message, ReportLevel reportLevel)
        {
            if ((int)this.ReportLevel <= (int)reportLevel)
            {
                if (this.File == null)
                {
                    throw new NullReferenceException("File path is not set!");
                }

                string messageToFile = this.Layout.ConstructMessage(message, reportLevel);

                if (!System.IO.File.Exists(this.File))
                {
                    System.IO.File.AppendAllText(this.File, messageToFile);
                }
                else
                {
                    System.IO.File.AppendAllText(this.File, Environment.NewLine + messageToFile);
                }                
            }
        }
    }
}
