namespace FootballBetting.ConsoleClient
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
            var context = new FootballBettingContext();
            context.Database.Initialize(true);
        }
    }
}
