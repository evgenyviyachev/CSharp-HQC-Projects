namespace BankSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<BankSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "BankSystem.Data.BankSystemContext";            
        }

        protected override void Seed(BankSystemContext context)
        {
            //AddOrUpdate
        }
    }
}
