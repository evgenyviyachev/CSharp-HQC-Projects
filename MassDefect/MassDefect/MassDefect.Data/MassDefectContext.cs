namespace MassDefect.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class MassDefectContext : DbContext
    {
        //IMPORTANT!
        //To run it -> CHANGE data source in all Connection Strings!

        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }
        
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<SolarSystem> SolarSystems { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Anomaly> Anomalies { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SolarSystem>()
                .HasMany(ss => ss.Stars)
                .WithRequired(s => s.SolarSystem)
                .Map(s =>
                {
                    s.MapKey("SolarSystemId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Planet>()
                .HasRequired(p => p.Sun)
                .WithMany(s => s.Planets)
                .Map(p =>
                {
                    p.MapKey("SunId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Planet>()
                .HasRequired(p => p.SolarSystem)
                .WithMany(ss => ss.Planets)
                .Map(p =>
                {
                    p.MapKey("SolarSystemId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Planet>()
                .HasMany(p => p.HomePeople)
                .WithRequired(hp => hp.HomePlanet)
                .Map(hp =>
                {
                    hp.MapKey("HomePlanetId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.OriginPlanet)
                .WithMany(op => op.OriginAnomalies)
                .Map(a =>
                {
                    a.MapKey("OriginPlanetId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.TeleportPlanet)
                .WithMany(tp => tp.TeleportAnomalies)
                .Map(a =>
                {
                    a.MapKey("TeleportPlanetId");
                })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasMany(a => a.People)
                .WithMany(p => p.Anomalies)
                .Map(pa =>
                {
                    pa.MapLeftKey("AnomalyId");
                    pa.MapRightKey("PersonId");
                    pa.ToTable("AnomalyVictims");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
