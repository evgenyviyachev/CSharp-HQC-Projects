namespace SOLID.Appenders
{
    using Contracts;
    using ReportLevels;

    public abstract class Appender : IAppender
    {
        private ReportLevel reportLevel = ReportLevel.Info;
        private ILayout layout;

        public Appender(ILayout layout)
        {
            this.Layout = layout;
        }

        public ReportLevel ReportLevel
        {
            get
            {
                return this.reportLevel;
            }
            set
            {
                this.reportLevel = value;
            }
        }

        public ILayout Layout
        {
            get
            {
                return this.layout;
            }

            private set
            {
                this.layout = value;
            }
        }

        public abstract void Append(string message, ReportLevel reportLevel);
    }
}
