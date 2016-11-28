namespace MassDefect.ConsoleClient
{
    using System;
    using Data;
    using Models;

    public class Program
    {
        public static void Main()
        {
            //IMPORTANT!
            //To run it -> CHANGE data source in all Connection Strings!

            var context = new MassDefectContext();
            context.Database.Initialize(true);
        }
    }
}
