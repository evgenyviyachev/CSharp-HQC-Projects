namespace BankSystem.ConsoleClient
{
    using Core;
    using Data;
    using Data.Migrations;
    using System.Data.Entity;

    public class Program
    {
        public static void Main()
        {
            var context = new BankSystemContext();
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<BankSystemContext, Configuration>());
            context.Database.Initialize(true);

            var engine = new Engine();

            engine.Run();
        }
    }
}
