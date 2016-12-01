namespace ACTester.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ACTesterContext : DbContext
    {
        public ACTesterContext()
            : base("name=ACTesterContext")
        {
        }

        public virtual DbSet<AirConditioner> AirConditioners { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
    }
}
